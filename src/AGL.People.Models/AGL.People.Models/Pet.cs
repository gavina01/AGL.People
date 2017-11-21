namespace AGL.People.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Pet Definition
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Pet Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type Of Pet defined By Enum
        /// </summary>
        [JsonProperty("type")]
        [EnumDataType(typeof(PetTypeEnum))]
        public PetTypeEnum Type { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {Type}";
        }
    }
}