using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarApp.Helpers
{
    public enum ServiceEnum
    {
        [Description("Λάδια")]
        Oil,
        [Description("Φίλτρο λαδιού")]
        OilFilter,
        [Description("Φίλτρο")]
        Filter,
        [Description("Φίλτρο Αέρος")]
        AirFilter,
        [Description("Mπουζί")]
        SparkPlug,
        [Description("Φίλτρο καμπίνας")]
        CabinFilter,
        [Description("Βαλβολίνες")]
        Valvolines,
        [Description("Φρένα Εμπρός")]
        FrontBrakes,
        [Description("Φρένα Πίσω")]
        RearBrakes,
        [Description("Ιμάντας Χρονισμού")]
        TimingBelt,
        [Description("Ιμάντας Δυναμό")]
        DynamoBelt,
        [Description("Υγρά Φρένων")]
        Brakesliquid

    }
}
