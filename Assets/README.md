# App Sorteos

## Estructura del Proyecto

```
Assets/
├── _Scripts/           # Todos los scripts de C#
│   ├── Core/          # Lógica central del sorteo
│   ├── UI/            # Scripts relacionados con la interfaz
│   └── Utils/         # Utilidades y helpers
├── Animations/        # Animaciones UI y efectos
├── Prefabs/          # Prefabs reutilizables
├── Resources/        # Recursos cargados dinámicamente
├── Scenes/           # Escenas del juego
│   ├── Main.unity    # Escena principal
│   └── Loading.unity # Pantalla de carga
├── Sprites/          # Imágenes y sprites 2D
│   ├── UI/           # Elementos de interfaz
│   └── Backgrounds/  # Fondos
└── UI/               # Assets de UI (prefabs, etc)
```

## Configuración del Proyecto
- Unity Version: 2022.3 LTS
- Plataforma: Android/iOS
- Orientación: Portrait
- Resolución Base: 1080x1920

## Convenciones de Nombrado
- Scripts: PascalCase (ej: SorteoManager)
- Variables: camelCase
- Constantes: UPPER_CASE
- Prefabs: Prefab_NombreDelPrefab 