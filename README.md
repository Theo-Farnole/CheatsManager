# Unity Cheats Manager

Make creation of cheats in Unity easier.

## Getting Started

### A new cheats
Go in Project Settings > Cheats.

### Edit cheat
Window dropdown > CheatsEditor
Tick or untick Cheats.

### Get cheat in game
```
CheatsManager.GetBool("myCheatKey");
```

### Set cheat in game
```
CheatsManager.SetBool("myCheatKey", true);
```

### What if cheat changed in game ?
```
void OnEnable()
{
	CheatsManager.OnCheatChanged += CheatsManager_OnCheatUpdated;
}

void CheatsManager_OnCheatUpdated(string key, CheatType)
{
	// Extra work...
}
```

## Author

* **Theo Farnole**  - [My Portfolio](tfarnole.me/)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details