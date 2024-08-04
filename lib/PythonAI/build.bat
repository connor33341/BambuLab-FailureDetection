@echo off
@rem Build [PythonAI.py | PythonAI.pyc] to [PythonAI.c | PythonAI.cpp] to PythonAI.pyd
setlocal

echo Building PythonAI.pyproj
echo Installing Requirements.txt
pip install -r C:\Users\conno\GitHub\BambuLab-FailureDetection\lib\PythonAI\requirements.txt
echo Requirements Installed
echo Moving to Temp
copy /y lib\PythonAI\PythonAI.py temp\PythonAI\PythonAIC.py
echo Converting PythonAI.py to PythonAI.c
cython --cplus -3 lib\PythonAI\PythonAI.py && (
	echo Cythonize C++ Successful
) || (
	echo Cythonize C++ Failed
)
cython -3 temp\PythonAI\PythonAIC.py && (
	echo Cythonize C Successful
) || (
	echo Cythonize C Failed
)
echo Building PythonAI.py using setup.py using C++
python lib\PythonAI\setup.py build_ext --inplace && (
	echo C++ Build Successful
) || (
	echo C++ Build Failed
	echo Trying with py instead of python
	py lib\PythonAI\setup.py build_ext --inplace && (
		echo C++ Build Successful
	) || (
		echo C++ Build Failed after 2 attempts
	)
)
echo Building PythonAI.pyc from PythonAI.py
python -m compileall temp\PythonAI\PythonAIC.py && (
	echo PYC Build Successful
) || (
	echo PYC Build Failed
	echo Trying with py instead of python
	py -m compileall temp\PythonAI\PythonAIC.py && (
		echo PYC Build Successful
	) || (
		echo PYC Build Failed
	)
)
echo Building PythonAI.py using setup.py using C
echo WARNING: Only works on developer builds of BambuLab-FailureDetection using legacy C PYD files, not bundled in normal builds.
echo WARNING: This will error, dont wory it built correctly.
python lib\PythonAI\setupc.py build_ext --inplace && (
	echo C Build Successful
) || (
	echo C Build Failed
	echo Trying with py instead of python
	py lib\PythonAI\setupc.py build_ext --inplace && (
		echo C Build Successful
	) || (
		echo C Build Failed after 2 attempts
	)
)
if errorlevel 1 (
	echo Non-Zero Error Level
	exit /b 0
)
exit /b 0
@rem exit 0
endlocal