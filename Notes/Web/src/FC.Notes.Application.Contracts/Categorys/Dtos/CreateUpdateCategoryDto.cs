using FC.Notes.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FC.Notes.Categorys.Dtos
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(CategoryConsts.MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(CategoryConsts.MaxDescriptionLength)]
        public virtual string Description { get; set; }
    }
}
