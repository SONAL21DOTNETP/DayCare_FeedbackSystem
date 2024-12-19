using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParentPreferenceFeedback.Models
{
    public class DaycareFormFocusArea
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("DaycareForm")]
        public int DaycareFormId { get; set; }  // Foreign key to DaycareFormModel

        public FocusArea FocusArea { get; set; }  // Enum property, not a navigation property

        public virtual DaycareFormModel DaycareForm { get; set; }
    }
}