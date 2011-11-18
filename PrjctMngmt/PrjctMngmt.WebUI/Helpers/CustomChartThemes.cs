using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjctMngmt.Helpers
{
    public static class CustomChartThemes
    {
        public const string PieTheme = @"<Chart BackColor=""#555"" BackGradientStyle=""TopBottom"" BorderColor=""181, 64, 1"" BorderWidth=""2"" BorderlineDashStyle=""Solid"" Palette=""None"" PaletteCustomColors='125,200,223; 195,88,80; 237,145,68; 137,107,167; 184,207,135; 246,248,103' AntiAliasing=""All"">
    <ChartAreas>
        <ChartArea Name=""Default"" _Template_=""All"" BackColor=""Transparent"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"">
            <Area3DStyle LightStyle=""Realistic"" Enable3D=""True"" Inclination=""50"" IsClustered=""False"" IsRightAngleAxes=""False"" Perspective=""10"" Rotation=""-30"" WallWidth=""0"" />
        </ChartArea>
    </ChartAreas>
</Chart>";
    }
}