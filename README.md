# Vessel

Vessel Framework is a Modern Cross-Platform Framework used to develop real-time interactive applications. It supports all modern Graphics APIs and offers a lot of flexibility in how it is structured

## What is Vessel
Vessel is a hobby project I'm working on as a current iteration of my game engine. The extremely ambitious and likely unobtainable goals which are planned for the future include a fully featured 3D render pipeline which is to include default shaders which are based on [Disney BRDF](https://blog.selfshadow.com/publications/s2012-shading-course/burley/s2012_pbs_disney_brdf_notes_v3.pdf), being heavily based on [Epic's implementation](https://de45xmedrsdbp.cloudfront.net/Resources/files/2013SiggraphPresentationsNotes-26915738.pdf) and [Frostbite's implementation](https://seblagarde.files.wordpress.com/2015/07/course_notes_moving_frostbite_to_pbr_v32.pdf), along with interactive 3D physics and high performance without sacrificing visual quality.
Vessel exposes a unified API agnostic abstraction for interfacing with platform specific libraries used in the development of real-time interactive applications such as videogames.

### Supported APIs

| API           | Windows | MacOS | Linux | Android |
| ------------- | ------- | ----- | ----- | ------- |
| OpenGL        | ✔       | ?     | ?     | ?       |
| Direct X 9    | ✔       | ?     | ?     | ?       |
| Direct X 11   | ✔       | ?     | ?     | ?       |
| Direct X 12 * | ❌       | ?     | ?     | ?       |
| Vulkan        | ✔       | ?     | ?     | ?       |

> *\* Direct X 12 is unsupported due to [Veldrid's](https://www.github.com/mellinoe/veldrid) lack of support for this API*
>
> *NB: Platforms other than Windows are untested*

## Projects using Vessel

This is a list of all projects that are using Vessel:

> WIP