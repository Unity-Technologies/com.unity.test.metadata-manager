# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.2.0-preview] - 2022-08-16
- Seperate tests into a seperate package to avoid nunit compilation errors we sporadically see where this package is used. Also, setup nunit refs in a better way and include moq so we can add better unit tests.

## [0.1.6-preview] - 2022-02-08
- Add args to handle custom input arguments used for XR tests

## [0.1.5-preview] - 2021-09-10
- Remove references to nunit dll

## [0.1.4-preview] - 2019-11-20

- capture playergraphicsapi so we can use this with our build metrics

## [0.1.3-preview] - 2019-11-05

- Convert CurrentSettings to static singleton and move into this package. This allows collection of metadata after the test starts, useful for parametric tests

## [0.1.2-preview] - 2019-10-01

- Add ProjectName

## [0.1.1-preview] - 2019-09-24

- Add reference

## [0.1.0-preview] - 2019-06-18

- This is the first release of *com.unity.xr.test.runtimesettings*.

