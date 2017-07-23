SET Project=%1
SET Branch=%2

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT_SOURCE (
  SET DEPLOYMENT_SOURCE=%~dp0%.
)

IF NOT DEFINED DEPLOYMENT_TARGET (
  SET DEPLOYMENT_TARGET=%ARTIFACTS%\wwwroot
)

echo Deploying files...
xcopy /Y "%DEPLOYMENT_SOURCE%\%Project%\Web.%Branch%.config" "%DEPLOYMENT_SOURCE%\%Project%\Web.config*"
deploy.cmd