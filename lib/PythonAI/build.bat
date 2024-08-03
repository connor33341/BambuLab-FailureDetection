@echo off
@rem Build PythonAI.py to PythonAI.c to PythonAI.pyd
setlocal

echo Building PythonAI.pyproj
echo Installing Requirements.txt
pip install requirements.txt
echo Requirements Installed
echo Building PythonAI.py using setup.py
python setup.py build_ext --inplace && (
	echo Build Successful
) || (
	echo Build Failed
	echo Trying with py instead of python
	py setup.py build_ext --inplace && (
		echo Build Successful
	) || (
		echo Build Failed after 2 attempts
	)
)

endlocal
pause