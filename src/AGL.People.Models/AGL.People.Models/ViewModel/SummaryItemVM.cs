namespace AGL.People.Models.ViewModel
{
    using System.Collections.Generic;

    /// <summary>
    /// VM For Summary Item By Gender By List Of Pet Names
    /// </summary>
    public class SummaryItemVM
    {
        /// <summary>
        /// Owner Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// List Of Pet Name
        /// </summary>
        public List<string> PetNames { get; set; }
    }
}