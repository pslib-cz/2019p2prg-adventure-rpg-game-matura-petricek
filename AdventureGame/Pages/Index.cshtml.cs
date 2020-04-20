using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AdventureGame.Models;
using AdventureGame.Services;

namespace AdventureGame.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Location _location;
        [BindProperty]
        public Location _test { get; set; }
        public IndexModel(Location location)
        {
            _location = location;
        }
        

        public void OnGet()
        {

        }
        public void OnPost()
        {
            
        }
    }
}
