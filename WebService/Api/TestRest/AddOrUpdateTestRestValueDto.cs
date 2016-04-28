using System.ComponentModel.DataAnnotations;

namespace Structura.WebApiOwinBoilerPlate.WebService.Api.TestRest
{
    public class AddOrUpdateTestRestValueDto
    {
        [Required]
        public string Value { get; set; }
    }
}