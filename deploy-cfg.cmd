@echo on
SET Project=%~1
SET ProjectPath=%~2
SET Solution=%~3
echo %branch%

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT_SOURCE (
  SET DEPLOYMENT_SOURCE=%~dp0%.
)

IF NOT DEFINED DEPLOYMENT_TARGET (
  SET DEPLOYMENT_TARGET=%ARTIFACTS%\wwwroot
)

echo Creating info...
echo commit:%SCM_COMMIT_ID% >"%DEPLOYMENT_SOURCE%\%ProjectPath%\info.txt"
echo. 2>>"%DEPLOYMENT_SOURCE%\%ProjectPath%\info.txt"
echo publishDate: %DATE% %TIME% >>"%DEPLOYMENT_SOURCE%\%ProjectPath%\info.txt"
echo. 2>>"%DEPLOYMENT_SOURCE%\%ProjectPath%\info.txt"
echo branch:%branch% >>"%DEPLOYMENT_SOURCE%\%ProjectPath%\info.txt"

echo Deploying files...
xcopy /Y "%DEPLOYMENT_SOURCE%\%ProjectPath%\Web.%branch%.config" "%DEPLOYMENT_SOURCE%\%ProjectPath%\Web.config*"

echo

deploy.cmd "%Project%" "%ProjectPath%" "%Solution%"