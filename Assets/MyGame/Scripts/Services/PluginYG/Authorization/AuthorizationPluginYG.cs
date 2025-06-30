using YG;

namespace Base.Services.PluginYG.Authorization
{
    public class AuthorizationPluginYG
    {
        public void TryAuthorize()
        {
            YG2.OpenAuthDialog();
        }
    }
}
