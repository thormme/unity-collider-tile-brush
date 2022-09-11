# unity-collider-tile-brush
A brush for tilemaps that allows fast editing of the collider type of a tile in Unity

## How to use this

You can use this in two different ways: downloading this repository or adding it to your project's Package Manager manifest.

Alternatively, you can pick and choose the scripts that you want by placing only these scripts in your project's `Assets` folder.

### Download

#### Setup
Download or clone this repository into your project in the folder `Packages/com.thormme.2d.tilemap.collider`.

### Package Manager Manifest

#### Requirements
[Git](https://git-scm.com/) must be installed and added to your path.

#### Setup
The following line needs to be added to your `Packages/manifest.json` file in your Unity Project under the `dependencies` section:

```json
"com.thormme.2d.tilemap.collider": "https://github.com/thormme/unity-collider-tile-brush.git#main"
```