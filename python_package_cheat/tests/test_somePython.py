import math
import sys
import os
import myPackage

"""

1. Why running script from local directory won't run __init__.py:
https://stackoverflow.com/questions/38758965/expanding-sys-path-via-init-py
Essentially __init__.py is only run/import if module is run/import, not if run/import local script is run.

2. Why we don't need to mess around with sys.path (discouraged).

Package is an object with associated __name__ in namespace.
By run/import tests.test_somePython you are finding (a) tests package
and initiliazing it. Secondly you add in the local namespace of where you called 'import'
the __name__ of all the  identifiers listed in the __init__ file.

These names are now in the top-level namespace of your script, and behave like any other object.

3. What is the namespace

It is a dictionary of strings that call your module as an object. Module being a python object.
So if you import math, then "math" is a key in locals() that leads to math.py module.
So if up import myPackage, then 'myPackage' is a key that leads to myPackage\__init__.py (b/c
directories are treated as modules if init is there).

So importing bascially adds all our modules to our local namespace.

"""

dict_locals = locals()

for k in dict_locals.keys():
    print k, '%%', dict_locals[k]

#lk = locals.keys()

#for k in lk:
#    print locals()[k]

print 'remove'

ll = locals()
print ll.has_key('__package__')
print ll['__package__']

#print globals()

dir(locals)


print 'start test'
def test_somePython():
    #print 'test.py start'
    for p in sys.path:
        print p
    print '--'
    print myPackage.somePython.f2k(10)
    assert myPackage.box.enum(1) == math.e * 1

test_somePython()

#from . import myPackage
#import myPackage
#import somePython
#from myPackage import somePython

# https://github.com/BillMills/pythonPackageLesson

#a = somePython.fahrToKelv(32)
#print a
