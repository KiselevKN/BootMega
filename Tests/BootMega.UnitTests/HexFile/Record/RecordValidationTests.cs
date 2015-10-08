using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile.Record.Validation;

namespace BootMega.UnitTests
{
    [TestClass]
    public class RecordValidationTests
    {
        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordCheckSumValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordСheckSumValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordCheckSumValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordСheckSumValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is HexRecordValidationException);
                
            };

            func(":120000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DD");
            func(":10C23000F04AF054BCF5204830592D02E018BB0300");
            func(":020001120000FC");
            func(":04000000FA00010200");
            func(":000000011F");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordLengthValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordLengthValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordLengthValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordLengthValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is HexRecordValidationException);

            };

            func(":0200000210000000EC");
            func(":02C22000F04EF05FF06CF07DCA0050C2F086F097DF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordNullOrEmptyValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordNullOrEmptyValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordNullOrEmptyValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordNullOrEmptyValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is ArgumentNullException);
            };

            func(String.Empty);
            func(null);
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordPatternValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordPatternValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordPatternValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordPatternValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is HexRecordValidationException);

            };

            func("020000020000FC");
            func(":020000081000EC");
            func(":020000020000F");
            func(":00000001");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordTypeValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordTypeValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordTypeValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordTypeValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is HexRecordValidationException);
            };

            func(":040000021000EC");
            func(":10C22001F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":020000020000FC00");
            func(":11000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordValidation()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordValidator();
                validator.Validate(record);
            };

            func(":020000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":10C23000F04AF054BCF5204830592D02E018BB03F9");
            func(":020000020000FC");
            func(":04000000FA00000200");
            func(":00000001FF");
        }

        [TestMethod]
        [TestCategory("RecordValidation")]
        public void HexRecordValidationException()
        {
            Action<string> func = (record) =>
            {
                IHexRecordValidator validator = new HexRecordValidator();
                bool hasException = false;
                Exception expectedException = null;
                try
                {
                    validator.Validate(record);
                }
                catch (Exception ex)
                {
                    hasException = true;
                    expectedException = ex;
                }

                Assert.IsTrue(hasException);
                Assert.IsTrue(expectedException is HexRecordValidationException);
            };

            func(":120000021000EC");
            func(":10C22000F04EF05FF06CF07DCA0050C2F086F097DD");
            func(":10C23000F04AF054BCF5204830592D02E018BB0300");
            func(":020001120000FC");
            func(":04000000FA00010200");
            func(":000000011F");

            func(":0200000210000000EC");
            func(":02C22000F04EF05FF06CF07DCA0050C2F086F097DF");

            func("020000020000FC");
            func(":020000081000EC");
            func(":020000020000F");
            func(":00000001");

            func(":040000021000EC");
            func(":10C22001F04EF05FF06CF07DCA0050C2F086F097DF");
            func(":020000020000FC00");
            func(":11000001FF");
        }
    }
}
