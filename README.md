# VerticalSlice

## Project Structure(Branches)

**Main** - Contains the last publically available release. Tested, clean and stable.

**Develop** - Contains the current in-development build for the next release. Contains only functional, tested and bugfree features added for the next update. Once you finish with a feature or fix branch, you merge it into develop.

**feature/** - Every time you start working on a new feature open a new branch titled feature/FeatureName, e.g. feature/SmokeGrenades, feature/Salute. Work and push on this branch until the feature is complete and tested, then you can merge into develop.

**fix/** - Every time you start fixing a bug open a new branch titled fix/BugName. Once it's fixed and tested push and merge into develop.


## Some tips

* Keep the feature and fix branches as small and localised in scope as possible. This allows you to complete different features in smaller steps and push to develop so other developers can have the latest MS.
* When working in Unity, make sure that you create a new scene for each branch you are working on! It is technically possible to work on the same scene with multiple people but not recommended.
* Coordinate who is working on what so as to avoid working on the same prefabs and/or scripts. Communication is key.
* Pull from develop as often as you can and make any code merges if necessary. You want to avoid doing this after many commits by other people have been made.
* Write clear and descriptive commit messages!
* Ask people on Discord if you need help.

## Guidelines

* NEVER work in the main branch/scene. The only time the main branch/scene is touched is when it is getting merges from develop and needs final implimentations.
* While working/testing, make sure you are on the correct branch/scene. (Do not work on the healthbar in the Database system branch/scene).
* NEVER, EVER work on the same branch as someone else at the same time!!!


## Basic Git Workflow

* **git pull origin develop** - Pull the latest changes from branch develop.

* **git branch branchName** - Create a new feature/ or fix/ branch.

* **git checkout branchName** - Switch to a branch.
* **git push --set-upstream origin branchName** - Push **new** branch to git after creation.
* Work on your feature or fix on the appropriate branch.
* Test your changes.
* Frequently commit to your branch in order to save and track your progress.

* **git status** - Check the status of your branch, files you modified or created.

* **git add** - Use git **add .** to add everything you changed to the commit.

* **git commit -m "Commit message"** - Write the commit message for your changes. Be concise and descriptive.

* **git push** - Push your changes to the branch.
* If you get stuck or want to switch to something else, you can always git checkout to another branch.
* Once you are finished with the fix or feature, commit and push everything to the branch.
