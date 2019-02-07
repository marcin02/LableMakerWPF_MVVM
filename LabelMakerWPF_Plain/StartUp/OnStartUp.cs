using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMakerWPF_Plain.StartUp
{
    public class OnStartUp
    {
        public void RunOnStartUp()
        {
            Restriction restriction = new Restriction();
            restriction.CheckRestriction();
            FirstRun firstRun = new FirstRun();
            firstRun.OnFirstRun();
        }
    }
}
