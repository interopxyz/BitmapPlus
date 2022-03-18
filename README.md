# BitmapPlus
A bitmap manipulation plugin for Grasshopper 3d

---

Bitmaps can be created pixel by pixel, imported, or generated and previewed live in the canvas. Both System Drawing Bitmaps and Bitmap Plus Image objects can then be used in a series of components. Procedural noise bitmaps can be generated using components built on the [Fastnoise algorithm](https://github.com/Auburn/FastNoiseLite).

Images can then be manipulated using over 100 methods made available from the [Accord Imaging Library](http://accord-framework.net/).

Multiple Images can be composited using the layer system in the [Dynamic Image Library](http://dynamicimage.apphb.com/) which enables the control of layer opacity, filtering, and masking as well as layer transformations and a series of modifiers.

Bitmaps can also be traced into a series of different geometry types including corner points, blob outlines, and limited shape identification using the Accord Imaging Library. General tracing of images is also available using the Potrace library

[Download Plugin at Food4Rhino](https://www.food4rhino.com/en/app/bitmap)

![definition](https://user-images.githubusercontent.com/25797596/158474370-efc29a78-e9a9-4bc8-8037-5fdcd8d05802.png)
![definition](https://user-images.githubusercontent.com/25797596/158474375-292435f5-bb42-4f4b-b363-f0ed2de681a1.png)
![definition](https://user-images.githubusercontent.com/25797596/158474352-c4f6d6dc-27cf-4f5d-9dcc-8f3ab64325ca.png)

## Learn More

[Plugin Documentation](https://interopxyz.gitbook.io/bitmap-plus/)

### Dependencies
 - [Rhinoceros 3d](https://www.rhino3d.com/)
 - [Rhinocommon](https://www.nuget.org/packages/RhinoCommon/5.12.50810.13095)
 - [Accord Imaging Library](http://accord-framework.net/)
 - [Dynamic Image](http://dynamicimage.apphb.com/)
 - [Fast Noise Lite](https://github.com/Auburn/FastNoiseLite)
 - [Potrace](http://potrace.sourceforge.net/)
