namespace AGL.People.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Person Model
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Constructore
        /// </summary>
        public Person()
        {
            this.Pets = new List<Pet>();
        }

        /// <summary>
        /// Person Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gender defined by Enum
        /// </summary>
        [EnumDataType(typeof(GenderTypeEnum))]
        public string Gender { get; set; }

        /// <summary>
        /// Name of Person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of owned Pets
        /// </summary>
        public List<Pet> Pets { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {Gender} {Age}";
        }
    }
}