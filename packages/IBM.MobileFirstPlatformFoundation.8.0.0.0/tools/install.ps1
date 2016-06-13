# Licensed Materials - Property of IBM
# 5725-I43 (C) Copyright IBM Corp. 2011, 2013. All Rights Reserved.
# US Government Users Restricted Rights - Use, duplication or
# disclosure restricted by GSA ADP Schedule Contract with IBM Corp.

param($installPath, $toolsPath, $package, $project)
# Set the Build Action to Content

# Copy the AuthWinRT DLLs to the respective locations.
Copy-Item $installPath\content\x86\AuthWinRT.dll -Destination $installPath\lib\netcore45\x86\AuthWinRT.dll
Copy-Item $installPath\content\x86\AuthWinRT.winmd -Destination $installPath\lib\netcore45\x86\AuthWinRT.winmd

Copy-Item $installPath\content\x86\AuthWinRTwp.dll -Destination $installPath\lib\netcore45\x86\AuthWinRTwp.dll
Copy-Item $installPath\content\x86\AuthWinRTwp.winmd -Destination $installPath\lib\netcore45\x86\AuthWinRTwp.winmd

Copy-Item $installPath\content\x64\AuthWinRT.dll -Destination $installPath\lib\netcore45\x64\AuthWinRT.dll
Copy-Item $installPath\content\x64\AuthWinRT.winmd -Destination $installPath\lib\netcore45\x64\AuthWinRT.winmd

Copy-Item $installPath\content\ARM\AuthWinRT.dll -Destination $installPath\lib\netcore45\ARM\AuthWinRT.dll
Copy-Item $installPath\content\ARM\AuthWinRT.winmd -Destination $installPath\lib\netcore45\ARM\AuthWinRT.winmd

Copy-Item $installPath\content\ARM\AuthWinRTwp.dll -Destination $installPath\lib\netcore45\ARM\AuthWinRTwp.dll
Copy-Item $installPath\content\ARM\AuthWinRTwp.winmd -Destination $installPath\lib\netcore45\ARM\AuthWinRTwp.winmd

#x86
$x86 = $project.ProjectItems.Item("x86")
$x86winmd = $x86.ProjectItems.Item("AuthWinRT.winmd")
$x86dll = $x86.ProjectItems.Item("AuthWinRT.dll")
$x86wpwinmd = $x86.ProjectItems.Item("AuthWinRTwp.winmd")
$x86wpdll = $x86.ProjectItems.Item("AuthWinRTwp.dll")

$x86dll.Delete()
$x86winmd.Delete()
$x86.Delete()
$x86wpwinmd.Delete()
$x86wpdll.Delete()

#x64
$x64 = $project.ProjectItems.Item("x64")
$x64dll = $x64.ProjectItems.Item("AuthWinRT.dll")
$x64winmd = $x64.ProjectItems.Item("AuthWinRT.winmd")

$x64dll.Delete()
$x64winmd.Delete()
$x64.Delete()

#ARM
$ARM = $project.ProjectItems.Item("ARM")
$ARMdll = $ARM.ProjectItems.Item("AuthWinRT.dll")
$ARMwinmd = $ARM.ProjectItems.Item("AuthWinRT.winmd")
$ARMwpdll = $ARM.ProjectItems.Item("AuthWinRTwp.dll")
$ARMwpwinmd = $ARM.ProjectItems.Item("AuthWinRTwp.winmd")

$ARMdll.Delete()
$ARMwinmd.Delete()
$ARM.Delete()
$ARMwpdll.Delete()
$ARMwpwinmd.Delete()
