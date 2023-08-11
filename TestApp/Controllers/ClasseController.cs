using Microsoft.AspNetCore.Mvc;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasseController: Controller
    {
        private readonly IClasseRepository _classeRepository;
        private readonly ISchoolRepository _schoolRepository;

        public ClasseController(IClasseRepository classeRepository, ISchoolRepository schoolRepository)
        {
            _classeRepository = classeRepository;
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        public ActionResult GetAllClasses() 
        {
            return Ok(_classeRepository.GetClasses());
        }

        [HttpGet("{id}")]
        public ActionResult GetClasse([FromRoute]int id)
        {
            var classe = _classeRepository.GetClasse(id);

            if (classe == null)
                return NotFound();

            return Ok(classe);
        }

        [HttpGet("title/{title}")]
        public ActionResult GetClasseByTitle(string title)
        {
            var classe = _classeRepository.GetClasseByTitle(title);

            if (classe == null)
                return NotFound();

            return Ok(classe);
        }

        [HttpGet("schools/{schoolId}")]
        public ActionResult GetSchoolClasses([FromRoute]int schoolId)
        {
            if(!_schoolRepository.SchoolExists(schoolId))
            {
                ModelState.AddModelError("", "School does not exist");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var classes = _classeRepository.GetSchoolClasses(schoolId);

            return Ok(classes);
        }

        [HttpPost]
        public ActionResult CreateClasse([FromBody]Classe classe, [FromQuery]int schoolId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = _schoolRepository.GetSchoolById(schoolId);

            if (school == null)
            {
                ModelState.AddModelError("", "School does not exist");
                return BadRequest(ModelState);
            }

            var newClasse = new Classe()
            {
                Title = classe.Title,
                School = school,
            };

            if(!_classeRepository.CreateClasse(newClasse))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return BadRequest(ModelState);
            }

            return Ok("Succefully created");
        }

        [HttpPut]
        public IActionResult UpdateClasse(Classe classe)
        {
            if (classe == null)
                return BadRequest();

            if (!_classeRepository.ClasseExists(classe.Id))
            {
                ModelState.AddModelError("", "Classe does not exist");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_classeRepository.UpdateClasse(classe))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClasse([FromRoute] int id)
        {
            if (!_classeRepository.ClasseExists(id))
            {
                ModelState.AddModelError("", "Classe does not exist");
                return NotFound(ModelState);
            }

            var classe = _classeRepository.GetClasse(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_classeRepository.DeleteClasse(classe))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully deleted");

        }
    }
}
