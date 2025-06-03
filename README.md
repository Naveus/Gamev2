# The Eternal Turn
*Orta çağ temalı karar tabanlı hayatta kalma oyunu*

## 🎮 Oyun Hakkında

**The Eternal Turn**, orta çağ döneminde veba salgını yaşanan bir köyde geçen, Reigns tarzında kart tabanlı karar verme oyunudur. Oyuncu bir köy lideri rolünde, dört ana stat'ı dengede tutarak köyün hayatta kalmasını sağlamaya çalışır.

### 📊 Ana Statlar
- 🏛️ **Halk Morali**: Halkın liderinize olan güveni
- ⛪ **Kilise Güveni**: Kilise ve din adamlarının desteği
- 💀 **Veba Yayılımı**: Salgının köydeki yaygınlığı
- 📦 **Kaynaklar**: Mali durum ve stoklar

### 🎯 Oyun Hedefleri
- **Zafer**: Veba yayılımını %0'a indirin
- **Yenilgi**: Herhangi bir stat %0'a düşerse veya veba %100'e ulaşırsa

## 🛠️ Teknik Detaylar

### Unity Gereksinimleri
- **Unity Versiyonu**: 2022.3 LTS veya üzeri
- **Platform**: PC (Windows/Mac/Linux)
- **Çözünürlük**: 1920x1080 (16:9)
- **Kontrol**: Sadece fare

### 🏗️ Proje Yapısı

```
Assets/
├── Scripts/
│   ├── Managers/
│   │   ├── StatManager.cs        # Stat yönetimi
│   │   ├── CardManager.cs        # Kart sistemi
│   │   └── GameManager.cs        # Oyun durumu
│   ├── Cards/
│   │   └── CardData.cs           # Kart verileri
│   ├── UI/
│   │   └── UICanvasSetup.cs      # UI kurulumu
│   └── Features/
├── Prefabs/                      # Prefab'lar
├── Sprites/                      # Görseller
├── Scenes/                       # Unity sahneleri
└── Resources/
    └── Localization/
        └── tr.json               # Türkçe çeviriler
```

### 🎨 UI Yapısı

Oyun, belgede belirtilen Reigns tarzında tasarlanmıştır:
- **Arka Plan**: 1920x1080 dekoratif görsel
- **Ana Panel**: Ortalanmış "telefon ekranı" görünümü (1080x1920)
- **Stat Panel**: Üst kısımda 4 stat göstergesi
- **Kart Paneli**: Ana içerik alanı
- **Seçim Butonları**: Alt kısımda A/B seçenekleri

## 🚀 Kurulum ve Çalıştırma

### 1. Unity'de Proje Açma
1. Unity Hub'ı açın
2. "Open" > Proje klasörünü seçin
3. Unity Editor'da proje açılacak

### 2. Sahne Hazırlama
1. `Assets/Scenes/` klasöründe yeni sahne oluşturun
2. Sahneye aşağıdaki GameObject'leri ekleyin:
   - **Canvas** (UI > Canvas)
   - **EventSystem** (UI > Event System)

### 3. Script Bağlama
1. Canvas'a şu scriptleri ekleyin:
   - `UICanvasSetup`
   - `StatManager`
   - `CardManager`
   - `GameManager`

### 4. Kart Verilerini Oluşturma
1. Project'te sağ tık > Create > The Eternal Turn > Card Data
2. Belgede belirtilen kartları tek tek oluşturun
3. CardManager'ın `allCards` listesine kartları atayın

## 📋 Geliştirme Rehberi

### Yeni Kart Ekleme
1. Project'te sağ tık > Create > The Eternal Turn > Card Data
2. Kart bilgilerini doldurun:
   - Başlık, açıklama
   - Bölüm numarası
   - Seçenekler ve etkileri
3. CardManager'ın kart listesine ekleyin

### Yeni Bölüm Ekleme
1. Yeni kartları ilgili bölüm numarasıyla oluşturun
2. `CardManager.GetChapterName()` metoduna bölüm adını ekleyin
3. Çeviri dosyasına bölüm metnini ekleyin

### Stat Sistemini Değiştirme
- `StatManager.cs` dosyasındaki değerleri düzenleyin
- Yeni statlar eklemek için UI'ı genişletin

## 🌐 Çoklu Dil Desteği

Oyun i18n sistemi kullanır:
- Türkçe: `Assets/Resources/Localization/tr.json`
- İngilizce: Gerektiğinde `en.json` dosyası eklenebilir

### Çeviri Anahtarları
```json
{
  "chapters": {
    "chapter1": {
      "cards": {
        "card1": {
          "title": "Kart Başlığı",
          "description": "Kart açıklaması"
        }
      }
    }
  }
}
```

## 🎯 Steam Yayın Hazırlığı

### Build Ayarları
1. **File > Build Settings**
2. **Platform**: PC, Mac & Linux Standalone
3. **Target Platform**: Windows x64
4. **Compression Method**: LZ4HC
5. **Development Build**: Kapalı (final build için)

### Steam Entegrasyonu
- Steam SDK entegrasyonu için Steamworks.NET eklentisi kullanılabilir
- Başarımlar (Achievements) sistemi eklenmesi önerilir
- Steam Workshop desteği (kart modları için)

## 🐛 Debug ve Test

### Console Komutları
- `[ContextMenu]` komutları ile testler:
  - StatManager: Random statlar
  - GameManager: Zorla zafer/yenilgi
  - CardManager: Sonraki kart/oyunu yeniden başlat

### Test Senaryoları
1. Tüm yenilgi koşullarını test edin
2. Kart geçişlerini kontrol edin
3. Zincirli kartların çalıştığını doğrulayın
4. UI'ın farklı çözünürlüklerde çalıştığını test edin

## 📝 Yapılacaklar

- [ ] Kart animasyonları
- [ ] Ses efektleri ve müzik
- [ ] Başarım sistemi
- [ ] Kayıt/yükleme sistemi
- [ ] Grafik iyileştirmeleri
- [ ] Steam entegrasyonu
- [ ] İngilizce çeviri

## 🤝 Katkıda Bulunma

1. Yeni kartlar ve senaryolar
2. UI/UX iyileştirmeleri
3. Çeviri desteği
4. Bug raporları ve düzeltmeler

## 📄 Lisans

Bu proje özel bir projedir. Steam'de ticari amaçla yayınlanacaktır.

---

**The Eternal Turn** - *Bir kararın kaderi değiştirebileceği orta çağ dünyasında hayatta kalma mücadelesi* 