@echo off
@rem Build [PythonAI.py | PythonAI.pyc] to [PythonAI.c | PythonAI.cpp] to PythonAI.pyd
setlocal

@rem Global Build Config
set "buildenable=false"

@rem Build Config
set "buildpyc=false"
set "buildpyd=true"
set "buildc=false"
set "buildcpp=true"
set "taskbuild=true"
set "debugpause=true"

@rem BuildTask
set "task=cppbuild"

@rem Build
echo Building PythonAI.pyproj
if %buildenable% == "false" (
	echo Global Build Disabled
	exit /b 0
)
if exist "\lib\PythonAI\requirements.txt" (
	echo Installing Requirements.txt
	pip install -r C:\Users\conno\GitHub\BambuLab-FailureDetection\lib\PythonAI\requirements.txt
	echo Requirements Installed
)
echo Moving to Temp
copy /y lib\PythonAI\PythonAI.py temp\PythonAI\PythonAIC.py
echo Running Main
if %taskbuild%=="true" (
	goto taskhandler
)
:taskhandler
echo Running TaskHandler
if %buildcpp%=="true" (
	set "task=cppbuild"
	set "buildcpp=false"
)
if %buildpyd%=="true" (
	set "task=cppsetup"
	set "buildpyd=false"
)
goto %task%
:cppbuild
cython --cplus -3 lib\PythonAI\PythonAI.py && (
	echo Cythonize C++ Successful
) || (
	echo Cythonize C++ Failed
)
@rem timeout /t 10 /nobreak
goto taskhandler
:cbuild
cython -3 temp\PythonAI\PythonAIC.py && (
	echo Cythonize C Successful
) || (
	echo Cythonize C Failed
)
:cppsetup
echo Building PythonAI.py using setup.py using C++
@rem timeout /t 10 /nobreak
python lib\PythonAI\setup.py build_ext --inplace && (
	echo C++ Build Successful
) || (
	echo C++ Build Failed
)
exit /b 0
goto end
:pycsetup
echo Building PythonAI.pyc from PythonAI.py
@rem timeout /t 10 /nobreak
python -m compileall temp\PythonAI\PythonAIC.py && (
	echo PYC Build Successful
) || (
	echo PYC Build Failed
	echo Trying with py instead of python
	@rem timeout /t 10 /nobreak
	py -m compileall temp\PythonAI\PythonAIC.py && (
		echo PYC Build Successful
	) || (
		echo PYC Build Failed
	)
)
:csetup
echo Building PythonAI.py using setup.py using C
echo WARNING: Only works on developer builds of BambuLab-FailureDetection using legacy C PYD files, not bundled in normal builds.
echo WARNING: This will error, dont wory it built correctly.
@rem timeout /t 10 /nobreak
python lib\PythonAI\setupc.py build_ext --inplace && (
	echo C Build Successful
) || (
	echo C Build Failed
	echo Trying with py instead of python
	@rem timeout /t 10 /nobreak
	py lib\PythonAI\setupc.py build_ext --inplace && (
		echo C Build Successful
	) || (
		echo C Build Failed after 2 attempts
	)
)
:end
if %debugpause%=="true"(
	pause
)
if errorlevel 1 (
	echo Non-Zero Error Level
	exit /b 0
)
exit /b 0
@rem exit 0
endlocal