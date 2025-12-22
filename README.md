# Language Options
- [EN](#2048-recreation-unity)
- [TR](#2048-recreation-unity-tr)

---

# 2048 Recreation (Unity)

This project is a high-quality educational recreation of the classic **2048** puzzle game, developed in Unity. It demonstrates advanced grid manipulation, procedural animations using Coroutines, and state-driven UI design.

## Technical Features

### 1. Grid & Tile Management
- **Abstract Grid System:** Implements a structured `TileGrid`, `TileRow`, and `TileCell` hierarchy to manage 2D coordinates within a Unity UI Canvas.
- **Dynamic Spawning:** Uses a smart search algorithm to find random empty cells, ensuring efficient tile generation even when the board is nearly full.
- **Merge Logic:** Features a robust "Check-and-Merge" system that handles tile values, doubling logic, and locking mechanisms to prevent multiple merges in a single move.

### 2. Procedural Animations
- **Coroutine-Based Movement:** Custom `AnimateMove` implementation using `Vector3.Lerp` for smooth tile transitions without relying on external heavy physics.
- **Visual States:** Utilizes `ScriptableObjects` (`TileState`) to decouple tile data from visuals, allowing easy customization of background and text colors for different tile values.

### 3. Game Flow & Logic
- **Input Direction Handling:** Advanced movement processing that iterates through the grid in specific orders based on the input direction (Up, Down, Left, Right) to ensure correct tile sliding.
- **Game Over Detection:** A recursive check that verifies if any possible merges remain on the board when all cells are occupied.
- **Event System:** Includes an `Action` based `gameOverCall` for decoupled communication between the board logic and UI managers.

---

# 2048 Recreation (Unity) - TR

Bu proje, klasik **2048** bulmaca oyununun Unity ile geliştirilmiş yüksek kaliteli bir yeniden yapımıdır. Gelişmiş ızgara (grid) manipülasyonu, Coroutine kullanılarak oluşturulan prosedürel animasyonlar ve durum odaklı (state-driven) arayüz tasarımını sergiler.

## Teknik Özellikler

### 1. Izgara ve Karakter (Tile) Yönetimi
- **Soyut Izgara Sistemi:** Unity UI Canvas içerisinde 2D koordinatları yönetmek için yapılandırılmış `TileGrid`, `TileRow` ve `TileCell` hiyerarşisi uygular.
- **Dinamik Oluşturma:** Tahta neredeyse doluyken bile verimli tile oluşturulmasını sağlayan, rastgele boş hücre bulma algoritması kullanır.
- **Birleştirme Mantığı:** Tek bir hamlede birden fazla birleşmeyi önlemek için kilit (locking) mekanizması ve sayı katlama mantığını içeren sağlam bir kontrol sistemi sunar.

### 2. Prosedürel Animasyonlar
- **Coroutine Tabanlı Hareket:** Harici fizik motoruna ihtiyaç duymadan, `Vector3.Lerp` kullanarak akıcı geçişler sağlayan özel `AnimateMove` fonksiyonu.
- **Görsel Durumlar:** Tile verilerini görsellikten ayırmak için `ScriptableObjects` (`TileState`) kullanır; bu sayede farklı değerler için arka plan ve metin renkleri kolayca özelleştirilebilir.

### 3. Oyun Akışı ve Mantığı
- **Girdi Yönü Yönetimi:** Karoların doğru şekilde kaymasını sağlamak için girdi yönüne (Yukarı, Aşağı, Sol, Sağ) bağlı olarak ızgarayı belirli sıralarla tarayan gelişmiş hareket işleme sistemi.
- **Oyun Bitiş Kontrolü:** Tüm hücreler dolduğunda tahtada yapılabilecek hamle kalıp kalmadığını doğrulayan özyinelemeli kontrol mekanizması.
- **Olay Sistemi:** Oyun mantığı ile arayüz yöneticileri arasında bağımsız iletişim sağlamak için `Action` tabanlı `gameOverCall` yapısını içerir.
