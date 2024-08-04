@echo off
@rem Property of the BambuLab-FailureDetection project
@rem connor33341

@rem Config
set "Debug=false" :: Enable Pause
set "RequirementsFile=requirements.txt" :: PIP Requirements
set "EntryPoint=PythonAI.py" :: Main PythonFile
set "SetupScript=setup.py" :: Setup Script
set "OutputFile=PythonAI.cp312-win_amd64.pyd" :: Created PYD
set "OutputDir=..\..\" :: BambuLab-FailureDetection
set "PYDName=PythonAI.pyd" :: Name of the PYD file in the primary dir
set "BuildCython=true" :: Generate C/C++ File using cython
set "BuildCythonLang=cpp" :: Lang C/C++
set "BuildSetup=true" :: Run the setup.py program to build the .pyd
set "CorrectDir=true" :: Move an rename the genereated .pyd

@rem Main Script
echo Building PythonAI.pyproj

if exist "%RequirementsFile%" (
	echo Installing RequirementsFile
	pip install -r "%RequirementsFile%"
) || (
	echo RequirementsFile not found
)

if "%BuildCython%" == "true" (
	echo Building Cython
	if "%BuildCythonLang%" == "cpp" (
		echo Building Cython using C++
		cython --cplus -3 "%EntryPoint%"
	)
	if "%BuildCythonLang%" == "c" (
		echo Building Cython using C
		cython -3 "%EntryPoint%"
	)
) || (
	echo Skipping Cython Build
)

if "%BuildSetup%" == "true" (
	echo Building PythonAI.pyd
	python "%SetupScript%" build_ext --inplace
) || (
	echo Skipping PythonAI.pyd
)

if "%CorrectDir%" == "true" (
	echo Correcting OutputDir
	copy "%OutputFile%" "%OutputDir%"
	ren "%OutputDir%%OutputFile%" "%PYDName%"
	del "%OutputFile%"
)

if "%Debug%"=="true" (
	pause
)
exit /b 0