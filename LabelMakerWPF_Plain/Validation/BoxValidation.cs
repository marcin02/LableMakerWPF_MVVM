using LabelMakerWPF_Plain.Models;
using System.Collections.Generic;

namespace LabelMakerWPF_Plain.Validation
{
    public class BoxValidation
    {
        public string Validation(Dictionary<string, BoxValidationModel> vd, string name)
        {
            string resultOK = null;
            string resultNOK = "Błąd";
            int blankBoxes = 0;

            if(name == "Company" && string.IsNullOrWhiteSpace(vd[name].Company))
            {
                return resultNOK;
            }

            //Throw error on Textbox for Item
            else if(name.StartsWith("I") && string.IsNullOrWhiteSpace(vd[name].Items) && vd[name].Quantity != 0)
            { 
                return resultNOK;
            }

            //Throw error on Textbox for Quantity
            else if(name.StartsWith("Q") && !string.IsNullOrWhiteSpace(vd[name].Items) && vd[name].Quantity == 0)
            {
                return resultNOK;
            }

            if (name!="Company")
            {
                foreach (KeyValuePair<string, BoxValidationModel> item in vd)
                {
                    if (string.IsNullOrWhiteSpace(item.Value.Items) && item.Value.Quantity == 0)
                    {
                        blankBoxes++;
                    }
                } 
            }

            if(blankBoxes==13)
            {
                return resultNOK;
            }

            return resultOK;
        }
    }
}
