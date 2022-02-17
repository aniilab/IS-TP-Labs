using System;
using System.Collections.Generic;

namespace MovieWebApplication
{
    public partial class Production
    {
        public Production()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string ProdName { get; set; } = null!;
        public string ProdCountry { get; set; } = null!;
        public int? MovieId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
