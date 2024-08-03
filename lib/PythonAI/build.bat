@echo off
@rem Build PythonAI.py to PythonAI.c to PythonAI.pyd
setlocal

echo Building PythonAI.pyproj
echo Installing Requirements.txt
pip install -r C:\Users\conno\GitHub\BambuLab-FailureDetection\lib\PythonAI\requirements.txt
echo Requirements Installed
echo Converting PythonAI.py to PythonAI.c
cython --cplus -3 lib\PythonAI\PythonAI.py && (
	echo Cythonize Successful
) || (
	echo Cythonize Failed
)
echo Building PythonAI.py using setup.py
python lib\PythonAI\setup.py build_ext --inplace && (
	echo Build Successful
) || (
	echo Build Failed
	echo Trying with py instead of python
	py lib\PythonAI\setup.py build_ext --inplace && (
		echo Build Successful
	) || (
		echo Build Failed after 2 attempts
	)
)
@rem exit 0
endlocal