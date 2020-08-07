using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MY.FeatureToggle;
using MY.FeatureToggle.Attributes;
using MY.FeatureToggle.Extensions;

namespace Demo.WebApi.Controllers
{

    public enum Features
    {
        [FeatureFlag("get-all-feature")]
        GetAllFeature,

        [FeatureFlag("get-single-feature")]
        GetFeature
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFeatureToggleProvider _featureToggleProvider;

        public ValuesController(IFeatureToggleProvider featureToggleProvider)
        {
            _featureToggleProvider = featureToggleProvider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var items = new List<string>() { "value1", "value2" };
            if (Features.GetAllFeature.IsEnable(_featureToggleProvider))
            {
                items.Add("Sample Feature is Enabled");
            }

            return items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var attributes = new Dictionary<string, object> {{"name", "muzammil"}};
            if (_featureToggleProvider.IsNotEnable(Features.GetFeature, attributes))
            {
                return "Feature is not enabled for muzammil.";
            }
            return "Feature is enabled for muzammil.";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
