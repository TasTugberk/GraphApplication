# GraphApplication

Çözüm altında 2 proje mevcuttur. İki proje de .NET 5.0 ile geliştirilmiştir.

İlk proje (GraphApplication) altında Graph geliştirmelerini yaptım. Graph'ı oluşturan verileri metin dosyalarında okuyorum. GraphApplication/TextFiles dizininde yer alan metin dosyalarından 'Vertices.txt' isimli dosyada her satır bir Vertex'i temsil etmektedir. 'Nodes.txt' dosyasında ise her satır SourceVertexIndex(zero-based) DestinationVertexIndex(zero-based) Cost formatında Vertex'ler arasındaki Node bilgilierini temsil etmektedir. Challenge üzerinde verilen Graph'tan farklı bir Graph elde etmek için metin dosyalarında ilgili değişiklikler yapılmalıdır. 

İkinci projede ise UnitTest geliştirmeleri yapılmıştır. NUnit (versiyon:3.13.1) ve Moq(versiyon:4.16.1) paketleri kullanılmıştır. VisualStudio içerisinden 'TestExplorer' ekranından testler çalıştırılabilir.

Her ne kadar gönderilen challenge'da integer tabanlı weighted-directed graph geliştirmesi istensede geliştirme sürecim boyunca interface ve generic type kullanarak uygulamyı değişikliklere kolayca adapte olabilecek hale getirmeye çalıştım.
