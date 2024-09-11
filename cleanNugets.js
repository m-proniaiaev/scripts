const fs = require('fs');
const path = require('path');

function deleteOldVersions(directory) {
  try {
  const subdirectories = fs.readdirSync(directory, { withFileTypes: true })
    .filter(dirent => dirent.isDirectory())
    .map(dirent => path.join(directory, dirent.name));

  const versionDirs = subdirectories.filter(subdir => /^\d+\.\d+\.\d+$/.test(path.basename(subdir)));

  if (versionDirs.length > 0) {
    versionDirs.sort((a, b) => b.localeCompare(a)); // Sort in descending order

    for (let i = 1; i < versionDirs.length; i++) {
      const versionDir = versionDirs[i];
      fs.rmdirSync(versionDir, { recursive: true, force: true });
      
      console.log(`Deleted: ${versionDir}`);
    }
  }

  for (const subdir of subdirectories) {
    deleteOldVersions(subdir);
  }
  }
  catch (e) {console.log(e)}

}

const rootDirectory = 'C:\\Users\\radioemptiness\\.nuget\\packages';

deleteOldVersions(rootDirectory);