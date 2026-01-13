// Phát nhạc nền theo tên
AudioManager.Instance.PlayBGM("MainTheme");

// Phát nhạc nền theo index
AudioManager.Instance.PlayBGM(0);

// Dừng nhạc
AudioManager.Instance.StopBGM();

// Tạm dừng/tiếp tục
AudioManager.Instance.PauseBGM();
AudioManager.Instance.ResumeBGM();

// Phát hiệu ứng âm thanh
AudioManager.Instance.PlaySFX("ButtonClick");
AudioManager.Instance.PlaySFX(0);

// Phát tại vị trí cụ thể (3D sound)
AudioManager.Instance.PlaySFXAtPoint("Explosion", transform.position);
