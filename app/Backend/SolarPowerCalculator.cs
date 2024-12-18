using System;

namespace Backend;

/// <summary>
/// Compute solar power @Lausanne based on date/time
/// 98% chatGPT
/// </summary>
static public class SolarPowerCalculator
{
    const double Latitude = 46.5191; // Latitude de Lausanne
    const double Longitude = 6.6323; // Longitude de Lausanne

    static public double CalculateSolarPower(DateTime date)
    {
        double hour = date.Hour + date.Minute / 60.0; // Heure locale en décimal
        double declination = GetSolarDeclination(date);
        double solarTime = GetSolarTime(hour, date);
        double solarAltitude = GetSolarAltitude(declination, solarTime);

        // Normaliser la puissance par rapport à l'altitude solaire maximale (21 juin à midi)
        double maxSolarAltitude = GetMaxSolarAltitude(Latitude);
        double power = Math.Max(0, solarAltitude / maxSolarAltitude);

        return power;
    }

    static double GetSolarDeclination(DateTime date)
    {
        // Approximation de la déclinaison solaire en degrés
        int dayOfYear = date.DayOfYear;
        return 23.45 * Math.Sin(2 * Math.PI * (284 + dayOfYear) / 365.0);
    }

    static double GetSolarTime(double localTime, DateTime date)
    {
        // Approximation de la correction solaire
        int dayOfYear = date.DayOfYear;
        double equationOfTime = 7.5 * Math.Sin(2 * Math.PI * (dayOfYear - 81) / 365.0);

        // Heure solaire (approximée)
        return localTime + equationOfTime / 60 - (Longitude / 15.0 - 1);
    }

    static double GetSolarAltitude(double declination, double solarTime)
    {
        // Calcul de l'angle horaire
        double hourAngle = 15 * (solarTime - 12);

        // Conversion en radians pour les calculs trigonométriques
        double latRad = Latitude * Math.PI / 180.0;
        double decRad = declination * Math.PI / 180.0;
        double haRad = hourAngle * Math.PI / 180.0;

        // Formule de l'élévation solaire
        double sinAlt = Math.Sin(latRad) * Math.Sin(decRad) + Math.Cos(latRad) * Math.Cos(decRad) * Math.Cos(haRad);
        return Math.Asin(sinAlt) * 180.0 / Math.PI; // Retour en degrés
    }

    static double GetMaxSolarAltitude(double latitude)
    {
        // Altitude solaire maximale le 21 juin à midi
        double declination = 23.45; // Déclinaison solaire maximale en degrés
        double latRad = latitude * Math.PI / 180.0;
        double decRad = declination * Math.PI / 180.0;

        // Formule de l'élévation solaire
        double sinAlt = Math.Sin(latRad) * Math.Sin(decRad) + Math.Cos(latRad) * Math.Cos(decRad);
        return Math.Asin(sinAlt) * 180.0 / Math.PI; // Retour en degrés
    }
}
