using PersonnummerKontroll;

namespace Personnummer_Kontroll.Test;

public class PersonnummerValidatorTest
{
    // ==================== GILTIGA PERSONNUMMER ====================
    
    [Fact]
    public void Validera_MedGiltigtPersonnummerIFormatYYMMDDXXXX_ReturnnarTrue()
    {
        // Arrangera
        string personnummer = "811218-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    [Fact]
    public void Validera_MedGiltigtPersonnummerIFormatYYYYMMDDXXXX_ReturnnarTrue()
    {
        // Arrangera
        string personnummer = "19811218-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    [Fact]
    public void Validera_MedGiltigtPersonnummerUtanBindestrek_ReturnnarTrue()
    {
        // Arrangera
        string personnummer = "8112189876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    // ==================== OGILTIGA FORMAT ====================

    [Fact]
    public void Validera_MedTommStreng_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedNullVärde_ReturnnarFalse()
    {
        // Arrangera
        string? personnummer = null;
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer ?? "");
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedFörFåSiffror_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811218-987";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedFörMångaSiffror_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811218-98765";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedOgiltigaTecken_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "81121A-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    // ==================== OGILTIGA DATUM ====================

    [Fact]
    public void Validera_MedOgiltiqMånad_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811318-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedOgiltigDag_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811232-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedDagNoll_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811200-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    // ==================== SAMORDNINGSNUMMER ====================

    [Fact]
    public void Validera_MedGiltiqtSamordningsnummer_ReturnnarTrue()
    {
        // Arrangera - Samordningsnummer har dag mellan 61-91
        string personnummer = "811261-9872";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    [Fact]
    public void Validera_MedSamordningsnummerDag61_ReturnnarTrue()
    {
        // Arrangera
        string personnummer = "811261-9872";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    // ==================== KONTROLLSIFFRA ====================

    [Fact]
    public void Validera_MedFelaktigKontrollsiffra_ReturnnarFalse()
    {
        // Arrangera
        string personnummer = "811218-9875";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.False(resultat);
    }

    [Fact]
    public void Validera_MedRättKontrollsiffra_ReturnnarTrue()
    {
        // Arrangera
        string personnummer = "811218-9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    // ==================== GetValideringsInfo ====================

    [Fact]
    public void GetValideringsInfo_MedTommStreng_ReturnnarFelmeddelande()
    {
        // Arrangera
        string personnummer = "";
        
        // Agera
        string resultat = PersonnummerValidator.GetValideringsInfo(personnummer);
        
        // Bekräfta
        Assert.Contains("saknas", resultat);
    }

    [Fact]
    public void GetValideringsInfo_MedOgiltigtFormat_ReturnnarFormatFelmeddelande()
    {
        // Arrangera
        string personnummer = "81121A-9876";
        
        // Agera
        string resultat = PersonnummerValidator.GetValideringsInfo(personnummer);
        
        // Bekräfta
        Assert.Contains("format", resultat);
    }

    [Fact]
    public void GetValideringsInfo_MedOgiltigtDatum_ReturnnarDatumFelmeddelande()
    {
        // Arrangera
        string personnummer = "811301-9876";
        
        // Agera
        string resultat = PersonnummerValidator.GetValideringsInfo(personnummer);
        
        // Bekräfta
        Assert.Contains("datum", resultat);
    }

    [Fact]
    public void GetValideringsInfo_MedFelaktigKontrollsiffra_ReturnnarKontrollsifraFelmeddelande()
    {
        // Arrangera
        string personnummer = "811218-9875";
        
        // Agera
        string resultat = PersonnummerValidator.GetValideringsInfo(personnummer);
        
        // Bekräfta
        Assert.Contains("kontrollsiffra", resultat);
    }

    [Fact]
    public void GetValideringsInfo_MedGiltigtPersonnummer_ReturnnarGiltigtMeddelande()
    {
        // Arrangera
        string personnummer = "811218-9876";
        
        // Agera
        string resultat = PersonnummerValidator.GetValideringsInfo(personnummer);
        
        // Bekräfta
        Assert.Contains("giltigt", resultat);
    }

    // ==================== SPECIALFALL ====================

    [Fact]
    public void Validera_MedMellanslag_StripparDemOchValiderar()
    {
        // Arrangera
        string personnummer = "8112 18 - 9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }

    [Fact]
    public void Validera_MedPlustecken_StripparDetOchValiderar()
    {
        // Arrangera
        string personnummer = "811218+9876";
        
        // Agera
        bool resultat = PersonnummerValidator.Validera(personnummer);
        
        // Bekräfta
        Assert.True(resultat);
    }
}
