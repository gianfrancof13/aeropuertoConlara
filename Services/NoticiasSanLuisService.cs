/* using HtmlAgilityPack;

public class NoticiaSanLuis
{
    public string Titulo { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
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
        var cards = doc.DocumentNode.SelectNodes("//div[contains(@class,'card')]");
        if (cards != null)
        {
            foreach (var card in cards)
            {
                var linkNode = card.SelectSingleNode(".//a[contains(@href,'agenciasanluis.com')]");
                var titleNode = card.SelectSingleNode(".//h4[contains(@class,'card-title')]");
                var imgNode = card.SelectSingleNode(".//img");
                string imagen = imgNode?.GetAttributeValue("src", string.Empty) ?? string.Empty;
                if (linkNode != null && titleNode != null)
                {
                    var titulo = titleNode.InnerText.Trim();
                    var href = linkNode.GetAttributeValue("href", "#");

                    // Si ya existe una noticia con el mismo título o URL y la nueva tiene imagen, reemplaza la anterior
                    var noticiaExistente = noticias.FirstOrDefault(n => n.Titulo == titulo || n.Url == href);

                    if (noticiaExistente != null)
                    {
                        // Si la nueva tiene imagen y la anterior no, reemplaza
                        if (!string.IsNullOrEmpty(imagen) && string.IsNullOrEmpty(noticiaExistente.Imagen))
                        {
                            noticiaExistente.Imagen = imagen;
                        }
                    }
                    else
                    {
                        noticias.Add(new NoticiaSanLuis { Titulo = titulo, Url = href, Imagen = imagen });
                    }

                    // Si ya tenemos 4 noticias, salimos del bucle
                    if (noticias.Count >= 3)
                        break;
                }
            }
        }
        return noticias;
    }
} */