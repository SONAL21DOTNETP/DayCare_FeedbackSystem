


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParentPreferenceFeedback.Models
{
    public class DaycareFormModel
    {
        [Key]
        public int Id { get; set; }

        // Parent Information
        [Required(ErrorMessage = "Parent name is required.")]
        [StringLength(100, ErrorMessage = "Parent name cannot exceed 100 characters.")]
        public string ParentName { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        // Child Information
        [Required(ErrorMessage = "Child's name is required.")]
        [StringLength(100, ErrorMessage = "Child's name cannot exceed 100 characters.")]
        public string ChildName { get; set; }

        [Required(ErrorMessage = "Child's age is required.")]
        [Range(0, 18, ErrorMessage = "Child's age must be between 0 and 18.")]
        public int ChildAge { get; set; }

        [Required(ErrorMessage = "Primary language is required.")]
        [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
        public string PrimaryLanguage { get; set; }

        [Required(ErrorMessage = "Requirements is required.")]
        [StringLength(500, ErrorMessage = "Requirements cannot exceed 500 characters.")]
        public string Requirements { get; set; }

        [Required(ErrorMessage = "Allergies is required.")]
        [StringLength(500, ErrorMessage = "Allergies or medical conditions cannot exceed 500 characters.")]
        public string Allergies { get; set; }

        [Required(ErrorMessage = "ComfortItems is required.")]
        [StringLength(200, ErrorMessage = "Comfort items cannot exceed 200 characters.")]
        public string ComfortItems { get; set; }

        [Required(ErrorMessage = "SleepingHabits is required.")]
        [StringLength(300, ErrorMessage = "Sleeping habits cannot exceed 300 characters.")]
        public string SleepingHabits { get; set; }

        [Required(ErrorMessage = "Meals is required.")]
        [StringLength(300, ErrorMessage = "Meal details cannot exceed 300 characters.")]
        public string Meals { get; set; }

        [Required(ErrorMessage = "BehavioralChallenges is required.")]
        [Display(Name = "Does your child have any specific behavioral challenges or things that trigger discomfort (e.g., separation anxiety, fear of loud noises)?")]
        public string BehavioralChallenges { get; set; }

        [Required(ErrorMessage = "Expectations is required.")]
        [StringLength(500, ErrorMessage = "Expectations cannot exceed 500 characters.")]
        public string Expectations { get; set; }

        [Required(ErrorMessage = "TeachingApproach is required.")]
        [StringLength(500, ErrorMessage = "Teaching approach cannot exceed 500 characters.")]
        public string TeachingApproach { get; set; }

        // Remove Required for these collections (because they are navigation properties)
        
        public virtual ICollection<DaycareFormFocusArea> DaycareFormFocusAreas { get; set; } = new List<DaycareFormFocusArea>();

        public virtual ICollection<DaycareFormCommunicationPreference> DaycareFormCommunicationPreferences { get; set; }

        // Drop-off and Pick-off times
        [Required(ErrorMessage = "Please specify a drop-off time.")]
        [Display(Name = "Drop-off")]
        public string Dropoff { get; set; }

        [Required(ErrorMessage = "Please specify a pick-off time.")]
        [Display(Name = "Pick-off")]
        public string Pickoff { get; set; }

        [Required(ErrorMessage = "PrimaryConcerns is required.")]
        [StringLength(500, ErrorMessage = "Primary concerns cannot exceed 500 characters.")]
        public string PrimaryConcerns { get; set; }

        [Required(ErrorMessage = "BehaviorTemperament is required.")]
        [StringLength(500, ErrorMessage = "Behavior temperament details cannot exceed 500 characters.")]
        public string BehaviorTemperament { get; set; }

        [Required(ErrorMessage = "SpecialRequests is required.")]
        [StringLength(500, ErrorMessage = "Special requests cannot exceed 500 characters.")]
        public string SpecialRequests { get; set; }

        [Required(ErrorMessage = "NotificationPreference is required.")]
        [StringLength(500, ErrorMessage = "Notification preference cannot exceed 500 characters.")]
        public string NotificationPreference { get; set; }

        


        [Required(ErrorMessage = "CulturalPractices is required.")]
        [StringLength(500, ErrorMessage = "Cultural practices cannot exceed 500 characters.")]
        public string CulturalPractices { get; set; }

        [Required(ErrorMessage = "LongTermGoals is required.")]
        [StringLength(500, ErrorMessage = "Long-term goals cannot exceed 500 characters.")]
        public string LongTermGoals { get; set; }

        [Required(ErrorMessage = "AdditionalInfo is required.")]
        [StringLength(500, ErrorMessage = "Additional information cannot exceed 500 characters.")]
        public string AdditionalInfo { get; set; }

    }

    // Enums for structured inputs
    public enum FocusArea
    {
        LanguageDevelopment,
        SocialSkills,
        PhysicalActivity,
        CreativeExpression,
        STEMEducation,
        CulturalAwareness
    }

    public enum CommunicationPreference
    {
        DailyUpdates,
        WeeklyReports,
        OccasionalEmails,
        DigitalCommunication,
        FaceToFaceMeetings
    }

    // Many-to-many relationship classes
    
}
