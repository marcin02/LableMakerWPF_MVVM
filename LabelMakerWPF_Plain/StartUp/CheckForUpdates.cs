using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.StartUp
{
    public class CheckForUpdates
    {
        public async Task Check()
        {
            using (var manager = UpdateManager.GitHubUpdateManager("https://github.com/marcin02/LableMakerWPF_MVVM"))
            {
                await manager.Result.UpdateApp();
            }
        }        
    }
}
