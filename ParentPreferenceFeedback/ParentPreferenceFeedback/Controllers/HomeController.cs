using ParentPreferenceFeedback.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ParentPreferenceFeedback.Controllers
{
    public class HomeController : Controller
    {
        private readonly DaycareFormModelContext _dbContext = new DaycareFormModelContext();
        private readonly string _connectString = ConfigurationManager.ConnectionStrings["DaycareFormModelContext"].ConnectionString;



        public ActionResult Index()
        {
            // Retrieve the first DaycareFormModel (if it exists)
            var manager = _dbContext.DaycareForms.FirstOrDefault();

            if (manager == null)
            {
                // If manager is null, initialize a new DaycareFormModel with empty collections
                manager = new DaycareFormModel
                {
                    ParentName = string.Empty,  
                    ContactNumber = string.Empty,  
                    Email = string.Empty, 
                    ChildName = string.Empty, 
                    ChildAge = 0,  
                    PrimaryLanguage = string.Empty,
                    Requirements = string.Empty,
                    Allergies = string.Empty,
                    ComfortItems = string.Empty,
                    SleepingHabits = string.Empty,
                    Meals = string.Empty,
                    BehavioralChallenges = string.Empty,
                    Expectations = string.Empty,
                    TeachingApproach = string.Empty,
                    Dropoff = string.Empty,
                    Pickoff = string.Empty,
                    PrimaryConcerns = string.Empty,
                    BehaviorTemperament = string.Empty,
                    SpecialRequests = string.Empty,
                    NotificationPreference = string.Empty,
                    CulturalPractices = string.Empty,
                    LongTermGoals = string.Empty,
                    AdditionalInfo = string.Empty,
                    DaycareFormFocusAreas = new List<DaycareFormFocusArea>(),
                    DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>()
                };
            }
            else
            {
                // If manager is not null, ensure the fields are cleared (optional if needed)
                manager.ParentName = string.Empty;
                manager.ContactNumber = string.Empty;
                manager.Email = string.Empty;
                manager.ChildName = string.Empty;
                manager.ChildAge = 0;
                manager.PrimaryLanguage = string.Empty;
                manager.Requirements = string.Empty;
                manager.Allergies = string.Empty;
                manager.ComfortItems = string.Empty;
                manager.SleepingHabits = string.Empty;
                manager.Meals = string.Empty;
                manager.BehavioralChallenges = string.Empty;
                manager.Expectations = string.Empty;
                manager.TeachingApproach = string.Empty;
                manager.Dropoff = string.Empty;
                manager.Pickoff = string.Empty;
                manager.PrimaryConcerns = string.Empty;
                manager.BehaviorTemperament = string.Empty;
                manager.SpecialRequests = string.Empty;
                manager.NotificationPreference = string.Empty;
                manager.CulturalPractices = string.Empty;
                manager.LongTermGoals = string.Empty;
                manager.AdditionalInfo = string.Empty;
                
                // Ensure all fields are cleared
                manager.DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>(); // Clear the list here as well
                manager.DaycareFormFocusAreas = new List<DaycareFormFocusArea>(); // Optionally clear the focus areas

            }

            if (manager == null)
            {
                // If manager is null, initialize a new DaycareFormModel with empty collections
                manager = new DaycareFormModel
                {
                    DaycareFormFocusAreas = new List<DaycareFormFocusArea>(),
                    DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>()
                };
            }
            else
            {
                // Ensure that collections are not null (important for binding)
                if (manager.DaycareFormFocusAreas == null)
                {
                    manager.DaycareFormFocusAreas = new List<DaycareFormFocusArea>();
                }

                if (manager.DaycareFormCommunicationPreferences == null)
                {
                    manager.DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>();
                }
            }

            // Prepare data for FocusArea checkboxes
            var focusAreas = Enum.GetValues(typeof(FocusArea))
                                 .Cast<FocusArea>()
                                 .Select(f => new SelectListItem
                                 {
                                     Text = f.ToString(),
                                     Value = f.ToString(),
                                     Selected = manager.DaycareFormFocusAreas.Any(x => x.FocusArea == f)
                                 }).ToList();

            // Prepare data for CommunicationPreference checkboxes
            var communicationPreferences = Enum.GetValues(typeof(CommunicationPreference))
                                                .Cast<CommunicationPreference>()
                                                .Select(c => new SelectListItem
                                                {
                                                    Text = c.ToString(),
                                                    Value = c.ToString(),
                                                    Selected = manager.DaycareFormCommunicationPreferences.Any(x => x.CommunicationPreference == c)
                                                }).ToList();

            // Pass the prepared lists to the view via ViewBag
            ViewBag.FocusAreas = focusAreas;
            ViewBag.CommunicationPreferences = communicationPreferences;

            // Return the view with the manager model
            return View(manager);
        }

        // POST: Home/Index
        [HttpPost]
        
        public ActionResult Index(DaycareFormModel model, string[] selectedFocusAreas, string[] selectedCommunicationPreferences)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    foreach (var focusArea in selectedFocusAreas)
                    {
                        var focusEnum = (FocusArea)Enum.Parse(typeof(FocusArea), focusArea);
                        model.DaycareFormFocusAreas.Add(new DaycareFormFocusArea { FocusArea = focusEnum });
                    }

                    // Ensure that the collection is initialized
                    //if (model.DaycareFormCommunicationPreferences == null)
                    //{
                        //model.DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>();
                    //}

                    // Add selected communication preferences to the model
                    foreach (var communicationPreference in selectedCommunicationPreferences)
                    {
                        // Parse the communication preference value from the checkbox input
                        var preferenceEnum = (CommunicationPreference)Enum.Parse(typeof(CommunicationPreference), communicationPreference);

                        // Add the parsed communication preference to the collection
                        model.DaycareFormCommunicationPreferences.Add(new DaycareFormCommunicationPreference
                        {
                            CommunicationPreference = preferenceEnum
                        });
                    }


                    _dbContext.DaycareForms.Add(model);
                    _dbContext.SaveChanges(); // Save the changes
                    TempData["Message"] = "Data successfully submitted!";
                    return RedirectToAction("Success");
                }
                
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationError in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationError.ValidationErrors)
                        {
                         
                            Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                        }
                    }
                    
                    ModelState.AddModelError("", "There was an error with your submission.");
                    return View(model); 
                }
            }
            else
            {
                return View(model); 
            }


        }

        public ActionResult Success()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }


        public ActionResult DisplayData()
        {
            List<DaycareFormModel> data = new List<DaycareFormModel>();

            using (SqlConnection conn = new SqlConnection(_connectString))
            {
                string query = @"
            SELECT df.*, dfa.FocusArea, dcp.CommunicationPreference
            FROM DaycareFormModels df
            LEFT JOIN DaycareFormFocusAreas dfa ON df.Id = dfa.DaycareFormId
            LEFT JOIN DaycareFormCommunicationPreferences dcp ON df.Id = dcp.DaycareFormId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Keep track of the current DaycareFormId to prevent duplicates
                        int currentDaycareFormId = -1;
                        DaycareFormModel currentModel = null;

                        while (reader.Read())
                        {
                            int daycareFormId = Convert.ToInt32(reader["Id"]);

                            // If we are reading a new DaycareFormModel, create a new model object
                            if (currentDaycareFormId != daycareFormId)
                            {
                                // If there's a model already, add it to the list
                                if (currentModel != null)
                                {
                                    data.Add(currentModel);
                                }

                                // Create a new DaycareFormModel
                                currentModel = new DaycareFormModel
                                {
                                    Id = daycareFormId,
                                    ParentName = reader["ParentName"].ToString(),
                                    ContactNumber = reader["ContactNumber"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    ChildName = reader["ChildName"].ToString(),
                                    ChildAge = reader["ChildAge"] != DBNull.Value ? Convert.ToInt32(reader["ChildAge"]) : 0,
                                    PrimaryLanguage = reader["PrimaryLanguage"].ToString(),
                                    Requirements = reader["Requirements"].ToString(),
                                    Allergies = reader["Allergies"].ToString(),
                                    ComfortItems = reader["ComfortItems"].ToString(),
                                    SleepingHabits = reader["SleepingHabits"].ToString(),
                                    Meals = reader["Meals"].ToString(),
                                    BehavioralChallenges = reader["BehavioralChallenges"].ToString(),
                                    Expectations = reader["Expectations"].ToString(),
                                    TeachingApproach = reader["TeachingApproach"].ToString(),
                                    Dropoff = reader["Dropoff"].ToString(),
                                    Pickoff = reader["Pickoff"].ToString(),
                                    PrimaryConcerns = reader["PrimaryConcerns"].ToString(),
                                    BehaviorTemperament = reader["BehaviorTemperament"].ToString(),
                                    SpecialRequests = reader["SpecialRequests"].ToString(),
                                    NotificationPreference = reader["NotificationPreference"].ToString(),
                                    CulturalPractices = reader["CulturalPractices"].ToString(),
                                    LongTermGoals = reader["LongTermGoals"].ToString(),
                                    AdditionalInfo = reader["AdditionalInfo"].ToString(),
                                    DaycareFormFocusAreas = new List<DaycareFormFocusArea>(), // Initialize the list
                                    DaycareFormCommunicationPreferences = new List<DaycareFormCommunicationPreference>() // Initialize if necessary
                                };

                                // Update the currentDaycareFormId to track the current model
                                currentDaycareFormId = daycareFormId;
                            }

                            // Add the FocusArea to the DaycareFormFocusAreas list
                            if (reader["FocusArea"] != DBNull.Value)
                            {
                                var focusArea = (FocusArea)Enum.Parse(typeof(FocusArea), reader["FocusArea"].ToString());
                                currentModel.DaycareFormFocusAreas.Add(new DaycareFormFocusArea
                                {
                                    DaycareFormId = daycareFormId,
                                    FocusArea = focusArea
                                });
                            }

                            // Add the CommunicationPreference to the DaycareFormCommunicationPreferences list
                            if (reader["CommunicationPreference"] != DBNull.Value)
                            {
                                var communicationPreference = (CommunicationPreference)Enum.Parse(typeof(CommunicationPreference), reader["CommunicationPreference"].ToString());
                                currentModel.DaycareFormCommunicationPreferences.Add(new DaycareFormCommunicationPreference
                                {
                                    DaycareFormId = daycareFormId,
                                    CommunicationPreference = communicationPreference
                                });
                            }
                        }

                        // Add the last model to the list after the loop ends
                        if (currentModel != null)
                        {
                            data.Add(currentModel);
                        }
                    }
                }
            }

            return View(data); // Return the data to the view
        }

    }
}
