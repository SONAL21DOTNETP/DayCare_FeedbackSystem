using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParentPreferenceFeedback.Models
{
    public class DaycareFormModelContext:DbContext
    {
        
        public DbSet<DaycareFormModel> DaycareForms { get; set; }
        public DbSet<DaycareFormFocusArea> DaycareFormFocusAreas { get; set; }
        public DbSet<DaycareFormCommunicationPreference> DaycareFormCommunicationPreferences { get; set; }


    }

}
