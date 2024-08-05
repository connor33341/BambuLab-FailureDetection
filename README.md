# BambuLab-FailureDetection
<!--META: RepoLoaderTags{PRERELEASE}-->
<!--META: Authors{connor33341}--->
<!--META: EncodingData{Contents: UTF8<string>, MetaData: UTF16<bstring>}-->
### Warning: Unfinished, project will be completed by next week

 Requires a model with a camera
---
 I'll write more about this later, basically it is a dotnet maui app that uses a pytorch model to detect 3d print errors.

  Building
  ---
  I would recomend against trying to build this, and instead download the built files. But if you want to make changes to the gui it is easier to download the .pyd rather than build it your self. Reasons for this is, the pyd does not build properly on my other devices, only the device with the orginal source builds a working pyd. But if needed modify and run the build.bat to produce a pyd and pyc file.

Platforms
---
While MAUI supports IOS, MacOSX, Linux, Android, and Windows. This project is limited to Windows only due to the use of the windows registry to store config data. As well as the use of compiling the python code to a windows only binary. If anyone is insane enough to attempt a version that can run on all the devices listed, submit a pull request.
