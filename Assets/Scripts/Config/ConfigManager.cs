using UnityEngine;
using System.Threading.Tasks;

public class ConfigManager : MonoBehaviour
{
    private GlobalConfig currentConfig;

    public async Task<GlobalConfig> LoadGlobalConfig()
    {
        if (currentConfig == null)
        {
            currentConfig = await MongoDBUtility.Instance.FetchGlobalConfig();
        }
        return currentConfig;
    }

    public GlobalConfig GetCurrentConfig()
    {
        return currentConfig;
    }

    public void ApplyGlovesConfig(GlovesConfig config)
    {
        Debug.Log($"Applying gloves config");
        Debug.Log($"BuzzThumb: {config.BuzzThumb}");
        Debug.Log($"ForceFeedbackThumb: {config.ForceFeedbackThumb}");
        Debug.Log($"ForceFeedbackIndex: {config.ForceFeedbackIndex}");
        Debug.Log($"ForceFeedbackMiddle: {config.ForceFeedbackMiddle}");
        Debug.Log($"ForceFeedbackRing: {config.ForceFeedbackRing}");
        Debug.Log($"ForceFeedbackPinky: {config.ForceFeedbackPinky}");


    }

    public void ApplyControllerKeyboardConfig(ControllerKeyboardConfig config)
    {
        Debug.Log($"Applying controller keyboard config");
        Debug.Log($"VibrationLevel: {config.VibrationLevel}");


    }


}
