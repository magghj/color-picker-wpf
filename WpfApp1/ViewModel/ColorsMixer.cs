using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public partial class ColorsMixer : ObservableObject
    {
        private string rgbToHexColor(int red, int green, int blue) => 
            $"#{red:X2}{green:X2}{blue:X2}";
        
        private void setRectangleColor(string hexString){
            this.RectangleColor = hexString;
        }
        [ObservableProperty]
        private string rectangleColor = "#777777";

        [ObservableProperty]
        private double red = 127;

        [ObservableProperty]
        private double green = 127;

        [ObservableProperty]
        private double blue = 127;

        private void updateColor()
        {
            var color = (rgbToHexColor((int)this.Red, (int)this.Green, (int)this.Blue));
            setRectangleColor(color);
            this.InputText = color;
        }

        partial void OnRedChanged(double value) => updateColor();
        partial void OnGreenChanged(double value) => updateColor();
        partial void OnBlueChanged(double value) => updateColor();

        [ObservableProperty]
        private string inputText = "#777777";

        [RelayCommand]
        private void buttonClickHandler(){
            if (!Helpers.isValidHexColorString(this.InputText)){
                return;
            }
            this.RectangleColor = this.InputText;
            (this.Red, this.Green, this.Blue) = Helpers.hexColorToRgb(this.InputText);
        }
    }

    class Helpers
    {
        public static bool isValidHexColorString(string value)
        {
            return Regex.IsMatch(value, @"^#[\dA-F]{6}$", RegexOptions.IgnoreCase);
        }

        public static (int, int, int) hexColorToRgb(string hexColor)
        {
            return (
                int.Parse(hexColor[1].ToString() + hexColor[2], System.Globalization.NumberStyles.HexNumber),
                int.Parse(hexColor[3].ToString() + hexColor[4], System.Globalization.NumberStyles.HexNumber),
                int.Parse(hexColor[5].ToString() + hexColor[6], System.Globalization.NumberStyles.HexNumber));
        }
    }
}