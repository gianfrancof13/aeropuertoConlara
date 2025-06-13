using HtmlAgilityPack;

public class NoticiaSanLuis
{
    public string Titulo { get; set; }
    public string Url { get; set; }
    public string Imagen { get; set; }
}

public class NoticiasSanLuisService
{
    private const string Url = "https://agenciasanluis.com/?s=aeropuerto";

    public async Task<List<NoticiaSanLuis>> ObtenerUltimasNoticiasAsync()
    {
        var noticias = new List<NoticiaSanLuis>();
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(Url);

        // Buscar todas las cards de noticias
        var cards = doc.DocumentNode.SelectNodes("//div[contains(@class,'card') and .//h4[contains(@class,'card-title')]]");
        if (cards != null)
        {
            foreach (var card in cards.Take(3)) // Solo las 3 primeras
            {
                var linkNode = card.SelectSingleNode(".//a[contains(@href,'agenciasanluis.com') and .//h4[contains(@class,'card-title')]]");
                var titleNode = card.SelectSingleNode(".//h4[contains(@class,'card-title')]");
                var imgNode = card.SelectSingleNode(".//img");
                string imagen = imgNode?.GetAttributeValue("src", null);
                if (linkNode != null && titleNode != null)
                {
                    var titulo = titleNode.InnerText.Trim();
                    var href = linkNode.GetAttributeValue("href", "#");
                    noticias.Add(new NoticiaSanLuis { Titulo = titulo, Url = href, Imagen = imagen });
                }
            }
        }
        return noticias;
    }
}