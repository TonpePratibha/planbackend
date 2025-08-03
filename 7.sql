
create database Toshfa_Acig;
use Toshfa_Acig;
CREATE TABLE PlanCrud ( PlanTemplateCode INT IDENTITY(1,1) PRIMARY KEY, PlanNameEnglish NVARCHAR(200) NOT NULL, PlanNameArabic NVARCHAR(200) NOT NULL, ClassName CHAR(1) NOT NULL, IsAutoHDFLoad BIT NOT NULL DEFAULT 0, MultipleSpouseAllowed BIT NOT NULL DEFAULT 0, PreExistingDiseaseAllowed BIT NOT NULL DEFAULT 0, MinAgeLimit INT NOT NULL, MaxAgeLimit INT NOT NULL, CONSTRAINT CK_Age_Limit CHECK (MinAgeLimit >= 0 AND MaxAgeLimit >= MinAgeLimit) );
select * from PlanCrud;
INSERT INTO PlanCrud ( PlanNameEnglish, PlanNameArabic, ClassName, IsAutoHDFLoad, MultipleSpouseAllowed, PreExistingDiseaseAllowed, MinAgeLimit, MaxAgeLimit ) VALUES ( 'Basic wsHealth Plan', 'uhh', 'A', 1, 0, 1, 19, 70 ); 
CREATE PROCEDURE GetAllPlans AS BEGIN SELECT * FROM PlanCrud ORDER BY PlanTemplateCode;END; -- Optional: Orders by primary key END; 
Exec GetAllPlans; 
CREATE PROCEDURE DeletePlan @PlanTemplateCode INT AS BEGIN DELETE FROM PlanCrud WHERE PlanTemplateCode = @PlanTemplateCode; END;
Exec DeletePlan 2;
CREATE PROCEDURE GetPlanById @PlanTemplateCode INT AS BEGIN SELECT * FROM PlanCrud WHERE PlanTemplateCode = @PlanTemplateCode; END; 
Exec GetPlanById 1;
CREATE PROCEDURE AddPlann @PlanTemplateCode INT, @PlanNameEnglish NVARCHAR(200), @PlanNameArabic NVARCHAR(200), @ClassName CHAR(1), @IsAutoHDFLoad BIT = 0, @MultipleSpouseAllowed BIT = 0, @PreExistingDiseaseAllowed BIT = 0, @MinAgeLimit INT, @MaxAgeLimit INT AS BEGIN INSERT INTO PlanCrud ( PlanNameEnglish, PlanNameArabic, ClassName, IsAutoHDFLoad, MultipleSpouseAllowed, PreExistingDiseaseAllowed, MinAgeLimit, MaxAgeLimit ) VALUES ( @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit ); END; 
CREATE PROCEDURE AddPlan @PlanNameEnglish NVARCHAR(200), @PlanNameArabic NVARCHAR(200), @ClassName CHAR(1), @IsAutoHDFLoad BIT = 0, @MultipleSpouseAllowed BIT = 0, @PreExistingDiseaseAllowed BIT = 0, @MinAgeLimit INT, @MaxAgeLimit INT AS BEGIN INSERT INTO PlanCrud ( PlanNameEnglish, PlanNameArabic, ClassName, IsAutoHDFLoad, MultipleSpouseAllowed, PreExistingDiseaseAllowed, MinAgeLimit, MaxAgeLimit ) VALUES ( @PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit ); END;
EXEC AddPlann @plante @PlanNameEnglish = 'Comprehensive Health Plan', @PlanNameArabic = 'swsws', @ClassName = 'B', @IsAutoHDFLoad = 1, @MultipleSpouseAllowed = 1, @PreExistingDiseaseAllowed = 0, @MinAgeLimit = 21, @MaxAgeLimit = 65; 
CREATE PROCEDURE UpdatePlan @PlanTemplateCode INT,@PlanNameEnglish NVARCHAR(200), @PlanNameArabic NVARCHAR(200), @ClassName CHAR(1), @IsAutoHDFLoad BIT, @MultipleSpouseAllowed BIT, @PreExistingDiseaseAllowed BIT, @MinAgeLimit INT, @MaxAgeLimit INT AS BEGIN UPDATE PlanCrud SET PlanNameEnglish = @PlanNameEnglish, PlanNameArabic = @PlanNameArabic, ClassName = @ClassName, IsAutoHDFLoad = @IsAutoHDFLoad, MultipleSpouseAllowed = @MultipleSpouseAllowed, PreExistingDiseaseAllowed = @PreExistingDiseaseAllowed, MinAgeLimit = @MinAgeLimit, MaxAgeLimit = @MaxAgeLimit WHERE PlanTemplateCode = @PlanTemplateCode; END;
EXEC UpdatePlan @PlanTemplateCode = 1, @PlanNameEnglish = 'Updated Health Plan', @PlanNameArabic = 'خطة الصحة المحدثة', @ClassName = 'B', @IsAutoHDFLoad = 1, @MultipleSpouseAllowed = 0, @PreExistingDiseaseAllowed = 1, @MinAgeLimit = 20, @MaxAgeLimit = 65;  
CREATE PROCEDURE AdddPlan @PlanNameEnglish NVARCHAR(200), @PlanNameArabic NVARCHAR(200), @ClassName CHAR(1), @IsAutoHDFLoad BIT = 0, @MultipleSpouseAllowed BIT = 0, @PreExistingDiseaseAllowed BIT = 0, @MinAgeLimit INT, @MaxAgeLimit INT, @NewId INT OUTPUT AS BEGIN SET NOCOUNT ON; 
INSERT INTO PlanCrud (PlanNameEnglish, PlanNameArabic, ClassName, IsAutoHDFLoad, MultipleSpouseAllowed, PreExistingDiseaseAllowed, MinAgeLimit, MaxAgeLimit) VALUES (@PlanNameEnglish, @PlanNameArabic, @ClassName, @IsAutoHDFLoad, @MultipleSpouseAllowed, @PreExistingDiseaseAllowed, @MinAgeLimit, @MaxAgeLimit); SET @NewId = SCOPE_IDENTITY(); END; 
Exec

CREATE PROCEDURE Addpplan
    @PlanNameEnglish NVARCHAR(200),
    @PlanNameArabic NVARCHAR(200),
    @ClassName CHAR(1),
    @IsAutoHDFLoad BIT = 0,
    @MultipleSpouseAllowed BIT = 0,
    @PreExistingDiseaseAllowed BIT = 0,
    @MinAgeLimit INT,
    @MaxAgeLimit INT,
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO PlanCrud (
        PlanNameEnglish,
        PlanNameArabic,
        ClassName,
        IsAutoHDFLoad,
        MultipleSpouseAllowed,
        PreExistingDiseaseAllowed,
        MinAgeLimit,
        MaxAgeLimit
    )
    VALUES (
        @PlanNameEnglish,
        @PlanNameArabic,
        @ClassName,
        @IsAutoHDFLoad,
        @MultipleSpouseAllowed,
        @PreExistingDiseaseAllowed,
        @MinAgeLimit,
        @MaxAgeLimit
    );

    -- Get the last inserted identity value and return in output parameter
    SET @Id = SCOPE_IDENTITY();
END;

ALTER PROCEDURE UpdatePlan
  @PlanTemplateCode INT,
  @PlanNameEnglish NVARCHAR(200),
  @PlanNameArabic NVARCHAR(200),
  @ClassName CHAR(1),
  @IsAutoHDFLoad BIT,
  @MultipleSpouseAllowed BIT,
  @PreExistingDiseaseAllowed BIT,
  @MinAgeLimit INT,
  @MaxAgeLimit INT
AS
BEGIN
    IF @MinAgeLimit < 0 OR @MaxAgeLimit < @MinAgeLimit
    BEGIN
        RAISERROR('Invalid age limits: MinAgeLimit must be >= 0 and MaxAgeLimit >= MinAgeLimit.', 16, 1);
        RETURN;
    END

    UPDATE PlanCrud
    SET PlanNameEnglish = @PlanNameEnglish,
        PlanNameArabic = @PlanNameArabic,
        ClassName = @ClassName,
        IsAutoHDFLoad = @IsAutoHDFLoad,
        MultipleSpouseAllowed = @MultipleSpouseAllowed,
        PreExistingDiseaseAllowed = @PreExistingDiseaseAllowed,
        MinAgeLimit = @MinAgeLimit,
        MaxAgeLimit = @MaxAgeLimit
    WHERE PlanTemplateCode = @PlanTemplateCode;
END
ALTER TABLE PlanCrud
DROP CONSTRAINT CK_Age_Limit;
