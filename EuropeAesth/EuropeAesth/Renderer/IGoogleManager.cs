using System;
using System.Collections.Generic;
using System.Text;
using EuropeAesth.Model;

namespace EuropeAesth.Renderer
{
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);

        void Logout();
    }
}
