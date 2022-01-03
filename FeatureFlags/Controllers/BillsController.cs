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

    [HttpGet("{feature}")]
    public async Task<IActionResult> Get(string feature)
    {
        var isEnabled = await _featureManager.IsEnabledAsync(feature);
        var featureNames = _featureManager.GetFeatureNamesAsync();

        return Ok(new
        {
            Message = $"O value of {feature} is {isEnabled}",
            FeatureNames = featureNames,
        });
    }

    [HttpGet("v2")]
    [FeatureGate(FeatureFlagsNames.BillFlag)]
    public IActionResult GetRoute2()
    {
        return Ok(new
        {
            Message = "it worked in the version 2.0!"
        });
    }
}
