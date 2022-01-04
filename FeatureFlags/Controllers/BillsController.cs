using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlags.Controllers;

[ApiController]
[Route("[controller]")]
public class BillsController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public BillsController(IFeatureManager featureManager) =>
        _featureManager = featureManager;

    [HttpGet]
    public IActionResult Get()
    {
        var featureNames = _featureManager.GetFeatureNamesAsync();

        return Ok(new
        {
            FeatureNames = featureNames,
        });
    }

    [HttpGet("v2")]
    [FeatureGate(FeatureFlagsNames.BillFlag)]
    public IActionResult GetRoute2()
    {
        return Ok(new
        {
            Message = $"it worked in the version v2"
        });
    }
}
