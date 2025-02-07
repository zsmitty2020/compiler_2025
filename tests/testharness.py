import os
import subprocess
import re
import os.path 
import json
import sys

os.chdir(os.path.dirname(__file__))

EXEC = r"C:\Users\Zach Smith\Desktop\Compiler\compiler_2025\bin\Debug\net8.0\test.exe"

passed=0

rex=re.compile(r"^t\d+\.txt$")
errrex = re.compile(r"(\d+)")

for x in sorted(os.listdir("testcases")):


	if rex.match(x):
		
		print(x,"...")
		
		with open(f"testcases/result-{x}","r") as fp:
			J=fp.read()
			J = json.loads(J)

		p = subprocess.run([EXEC,os.path.join("testcases",x) ],
			capture_output=True)
		j = p.stdout.decode()
		print(j)
		assert p.returncode == J["returncode"], "Return codes don't match"
		
		if p.returncode != 0:
			m = errrex.search(j)
			assert m
			errorline = int(m.group(1))
			assert errorline == J["error"]
		else:
			j = json.loads(j)
			for i in range(0, len(j)):
				print(J["tokens"][i], "=", j[i])
				assert J["tokens"][i] == j[i]

			#assert J["tokens"] == j
			
		
		print("OK")
		passed += 1
		
print(passed,"tests passed")
