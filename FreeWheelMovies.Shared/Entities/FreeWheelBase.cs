using System;
using System.Runtime.Serialization;

namespace FreeWheelMovies.Shared.Entities
{
    public class FreeWheelBase
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// Active or Deleted (Internal)
        /// </summary>
        [IgnoreDataMember]
        public bool IsActive { get; set; }
    }
}
