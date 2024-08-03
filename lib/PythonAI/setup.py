from setuptools import setup, Extension
from Cython.Build import cythonize
import numpy
#CWD: BambuLab-FailureDetection

ext_modules = [
    Extension(
        "PythonAI",
        sources=["lib\PythonAI\PythonAI.cpp"],
        include_dirs=[numpy.get_include()]
    )
]
setup(
        name="PythonAI",
        ext_modules=cythonize(ext_modules),
        install_requires=["torch","numpy"]
)