using EA.BlogProject.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Entities.Concrete
{
    // int primary key ile oluşsun dedik, string ve guid olarak ta tutulabilir
    // identity tablosuna ek kolon ekleyebiliriz.
    public class User : IdentityUser<int>
    { 
        public string Picture { get; set; } 
        public ICollection<Article> Articles { get; set; }
    }
}
