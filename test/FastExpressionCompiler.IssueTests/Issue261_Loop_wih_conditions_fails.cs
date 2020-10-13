using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using System.Text;
using SysExpr = System.Linq.Expressions.Expression;

#if LIGHT_EXPRESSION
using static FastExpressionCompiler.LightExpression.Expression;
namespace FastExpressionCompiler.LightExpression.IssueTests
#else
using static System.Linq.Expressions.Expression;
namespace FastExpressionCompiler.IssueTests
#endif
{
    public class Issue261_Loop_wih_conditions_fails : ITest
    {
        public int Run()
        {
            Test_DictionaryTest_StringDictionary();

            Test_the_big_re_engineering_test_from_the_Apex_Serializer_with_the_simple_mock_arguments();

            Test_assignment_with_the_block_on_the_right_side_with_just_a_constant();
            Test_assignment_with_the_block_on_the_right_side();

#if LIGHT_EXPRESSION
            FindMethodOrThrow_in_the_class_hierarchy();

            Test_find_generic_method_with_the_generic_param();

            Can_make_convert_and_compile_binary_equal_expression_of_different_types();

            Test_method_to_expression_code_string();

            Test_nested_generic_type_output();
            Test_triple_nested_non_generic();
            Test_triple_nested_open_generic();
            Test_non_generic_classes();

            return 12;
#else
            Should_throw_for_the_equal_expression_of_different_types();

            return 5;
#endif
        }

        [Test]
        public void Test_assignment_with_the_block_on_the_right_side_with_just_a_constant()
        {
            var result = Parameter(typeof(int), "result");
            var temp = Parameter(typeof(int), "temp");
            var e = Lambda<Func<int>>(
                Block(
                    new ParameterExpression[] { result },
                    Assign(result, Block(
                        new ParameterExpression[] { temp },
                        Assign(temp, Constant(42)),
                        temp
                    )),
                    result
                )
            );

            e.PrintCSharpString();

            var fSys = e.CompileSys();
            Assert.AreEqual(42, fSys());

            var f = e.CompileFast(true);
            Assert.AreEqual(42, f());
        }

        [Test]
        public void Test_assignment_with_the_block_on_the_right_side()
        {
            var result = Parameter(typeof(int[]), "result");
            var temp = Parameter(typeof(int), "temp");
            var e = Lambda<Func<int[]>>(
                Block(
                    new ParameterExpression[] { result },
                    Assign(result, NewArrayBounds(typeof(int), Constant(1))),
                    Assign(
                        ArrayAccess(result, Constant(0)),
                        Block(
                            new ParameterExpression[] { temp },
                            Assign(temp, Constant(42)),
                            temp
                    )),
                    result
                )
            );

            e.PrintCSharpString();

            var fSys = e.CompileSys();
            Assert.AreEqual(42, fSys()[0]);

            var f = e.CompileFast(true);
            Assert.AreEqual(42, f()[0]);
        }

        [Test]
        public void Test_DictionaryTest_StringDictionary()
        {
            var p = new ParameterExpression[3]; // the parameter expressions 
            var e = new Expression[6]; // the unique expressions 
            var l = new LabelTarget[1]; // the labels 

            var expr = Lambda(/*$*/
              typeof(WriteMethods<FieldInfoModifier.TestReadonly, BufferedStream, Settings_827720117>.WriteSealed),
              e[0] = Block(
                typeof(void),
                new ParameterExpression[0],
                e[1] = Call(
                  p[0] = Parameter(typeof(BufferedStream).MakeByRefType(), "stream"),
                  typeof(BufferedStream).GetMethods().Single(x =>
                  x.Name == "ReserveSize" && !x.IsGenericMethod && x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) })),
                  e[2] = Constant((int)4)),
                e[3] = Call(
                  p[0]/*(BufferedStream stream)*/,
                  typeof(BufferedStream).GetMethods().Where(x =>
                  x.Name == "Write" && x.IsGenericMethod && x.GetGenericArguments().Length == 1).Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x)
                .Single(x => x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) })),
                  e[4] = Field(
                    p[1] = Parameter(typeof(FieldInfoModifier.TestReadonly), "source"),
                    typeof(FieldInfoModifier.TestReadonly).GetTypeInfo().GetDeclaredField("Value"))),
                e[5] = Label(l[0] = Label(typeof(void), "finishWrite"))),
              p[1]/*(FieldInfoModifier.TestReadonly source)*/,
              p[0]/*(BufferedStream stream)*/,
              p[2] = Parameter(typeof(Binary<BufferedStream, Settings_827720117>), "io"));

            expr.PrintCSharpString();

            var fs = (WriteMethods<FieldInfoModifier.TestReadonly, BufferedStream, Settings_827720117>.WriteSealed)expr.CompileSys();
            fs.PrintIL();

            var f = (WriteMethods<FieldInfoModifier.TestReadonly, BufferedStream, Settings_827720117>.WriteSealed)expr.CompileFast(true);
            f.PrintIL();
        }

        [Test]
        public void Test_the_big_re_engineering_test_from_the_Apex_Serializer_with_the_simple_mock_arguments()
        {
            var p = new ParameterExpression[7]; // the parameter expressions 
            var e = new Expression[56]; // the unique expressions 
            var l = new LabelTarget[3]; // the labels 
            var expr = Lambda(/*$*/
              typeof(ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed),
              e[0] = Block(
                typeof(ConstructorTests.Test[]),
                new[] {
              p[0]=Parameter(typeof(ConstructorTests.Test[]), "result")
                },
                e[1] = Empty(),
                e[2] = Block(
                  typeof(void),
                  new[] {
                p[1]=Parameter(typeof(int), "length0")
                  },
                  e[3] = Call(
                    p[2] = Parameter(typeof(BufferedStream).MakeByRefType(), "stream"),
                    typeof(BufferedStream).GetMethods().Single(x => x.Name == "ReserveSize" && !x.IsGenericMethod && x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) })),
                    e[4] = Constant((int)4)),
                  e[5] = MakeBinary(ExpressionType.Assign,
                    p[1]/*(int length0)*/,
                    e[6] = Call(
                      p[2]/*(BufferedStream stream)*/,
                      typeof(BufferedStream).GetMethods().Single(x => x.Name == "Read" && x.IsGenericMethod && x.GetGenericArguments().Length == 1 && x.GetParameters().Length == 0).MakeGenericMethod(typeof(int)))),
                  e[7] = MakeBinary(ExpressionType.Assign,
                    p[0]/*(ConstructorTests.Test[] result)*/,
                    e[8] = NewArrayBounds(
                      typeof(ConstructorTests.Test),
                      p[1]/*(int length0)*/)),
                  e[9] = Call(
                    e[10] = Call(
                      p[3] = Parameter(typeof(Binary<BufferedStream, Settings_827720117>), "io"),
                      typeof(Binary<BufferedStream, Settings_827720117>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == "get_LoadedObjectRefs" && !x.IsGenericMethod && x.GetParameters().Length == 0)),
                    typeof(List<object>).GetMethods().Single(x => x.Name == "Add" && !x.IsGenericMethod && x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(object) })),
                    p[0]/*(ConstructorTests.Test[] result)*/),
                  e[11] = Block(
                    typeof(void),
                    new[] {
                  p[4]=Parameter(typeof(int), "index0"),
                  p[5]=Parameter(typeof(ConstructorTests.Test), "tempResult")
                    },
                    e[12] = Block(
                      typeof(void),
                      new ParameterExpression[0],
                      e[13] = MakeBinary(ExpressionType.Assign,
                        p[4]/*(int index0)*/,
                        e[14] = MakeBinary(ExpressionType.Subtract,
                          p[1]/*(int length0)*/,
                          e[15] = Constant((int)1))),
                      e[16] = Loop(
                        e[17] = Condition(
                          e[18] = MakeBinary(ExpressionType.LessThan,
                            p[4]/*(int index0)*/,
                            e[19] = Constant((int)0)),
                          e[20] = MakeGoto(GotoExpressionKind.Break,
                            l[0] = Label(typeof(void)),
                            null,
                            typeof(void)),
                          e[21] = Block(
                            typeof(int),
                            new ParameterExpression[0],
                            e[22] = Block(
                              typeof(ConstructorTests.Test),
                              new ParameterExpression[0],
                              e[1]/*Default*/,
                              e[23] = MakeBinary(ExpressionType.Assign,
                                e[24] = ArrayAccess(
                                  p[0]/*(ConstructorTests.Test[] result)*/, new Expression[] {
                                p[4]/*(int index0)*/}),
                                e[25] = Block(
                                  typeof(ConstructorTests.Test),
                                  new ParameterExpression[0],
                                  e[26] = MakeBinary(ExpressionType.Assign,
                                    p[5]/*(ConstructorTests.Test tempResult)*/,
                                    e[27] = Default(typeof(ConstructorTests.Test))),
                                  e[28] = Call(
                                    p[2]/*(BufferedStream stream)*/,
                                    typeof(BufferedStream).GetMethods().Single(x => x.Name == "ReserveSize" && !x.IsGenericMethod && x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) })),
                                    e[29] = Constant((int)5)),
                                  e[30] = Condition(
                                    e[31] = MakeBinary(ExpressionType.Equal,
                                      e[32] = Call(
                                        p[2]/*(BufferedStream stream)*/,
                                        typeof(BufferedStream).GetMethods().Single(x => x.Name == "Read" && x.IsGenericMethod && x.GetGenericArguments().Length == 1 && x.GetParameters().Length == 0).MakeGenericMethod(typeof(byte))),
                                      e[33] = Constant((byte)0)),
                                    e[34] = MakeGoto(GotoExpressionKind.Goto,
                                      l[1] = Label(typeof(void), "skipRead"),
                                      null,
                                      typeof(void)),
                                    e[1]/*Default*/,
                                    typeof(void)),
                                  e[35] = Block(
                                    typeof(void),
                                    new[] {
                                  p[6]=Parameter(typeof(int), "refIndex")
                                    },
                                    e[36] = MakeBinary(ExpressionType.Assign,
                                      p[6]/*(int refIndex)*/,
                                      e[37] = Call(
                                        p[2]/*(BufferedStream stream)*/,
                                        typeof(BufferedStream).GetMethods().Single(x => x.Name == "Read" && x.IsGenericMethod && x.GetGenericArguments().Length == 1 && x.GetParameters().Length == 0).MakeGenericMethod(typeof(int)))),
                                    e[38] = Condition(
                                      e[39] = MakeBinary(ExpressionType.NotEqual,
                                        p[6]/*(int refIndex)*/,
                                        e[40] = Constant((int)-1)),
                                      e[41] = Block(
                                        typeof(void),
                                        new ParameterExpression[0],
                                        e[42] = MakeBinary(ExpressionType.Assign,
                                          p[5]/*(ConstructorTests.Test tempResult)*/,
                                          e[43] = Convert(
                                            e[44] = MakeIndex(
                                              e[45] = Call(
                                                p[3]/*(Binary<BufferedStream, Settings_827720117> io)*/,
                                                typeof(Binary<BufferedStream, Settings_827720117>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == "get_LoadedObjectRefs" && !x.IsGenericMethod && x.GetParameters().Length == 0)),
                                              typeof(List<object>).GetTypeInfo().GetDeclaredProperty("Item"), new Expression[] {
                                            e[46]=Decrement(
                                              p[6]/*(int refIndex)*/)}),
                                            typeof(ConstructorTests.Test))),
                                        e[47] = MakeGoto(GotoExpressionKind.Goto,
                                          l[1]/* skipRead */,
                                          null,
                                          typeof(void))),
                                      e[1]/*Default*/,
                                      typeof(void))),
                                  e[48] = MakeBinary(ExpressionType.Assign,
                                    p[5]/*(ConstructorTests.Test tempResult)*/,
                                    e[49] = New(/*0 args*/
                                      typeof(ConstructorTests.Test).GetTypeInfo().DeclaredConstructors.ToArray()[0], new Expression[0])),
                                  e[50] = Call(
                                    e[51] = Call(
                                      p[3]/*(Binary<BufferedStream, Settings_827720117> io)*/,
                                      typeof(Binary<BufferedStream, Settings_827720117>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == "get_LoadedObjectRefs" && !x.IsGenericMethod && x.GetParameters().Length == 0)),
                                    typeof(List<object>).GetMethods().Single(x => x.Name == "Add" && !x.IsGenericMethod && x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(object) })),
                                    p[5]/*(ConstructorTests.Test tempResult)*/),
                                  e[52] = Label(l[1]/* skipRead */),
                                  p[5]/*(ConstructorTests.Test tempResult)*/))),
                            e[53] = Label(l[2] = Label(typeof(void), "continue0")),
                            e[54] = MakeBinary(ExpressionType.Assign,
                              p[4]/*(int index0)*/,
                              e[55] = Decrement(
                                p[4]/*(int index0)*/))),
                          typeof(void)),
                        l[0]/* void_60623824 */)))),
                p[0]/*(ConstructorTests.Test[] result)*/),
              p[2]/*(BufferedStream stream)*/,
              p[3]/*(Binary<BufferedStream, Settings_827720117> io)*/);

            var s = string.Empty;
            expr.PrintCSharpString(ref s);
            StringAssert.DoesNotContain("return index0", s);

            var fs = (ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed)expr.CompileSys();
            fs.PrintIL();

            var stream = new BufferedStream();
            var binary = new Binary<BufferedStream, Settings_827720117>();
            var x = fs(ref stream, binary);
            Assert.IsNotNull(x);

            var f = (ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed)expr.CompileFast();
            f.PrintIL();
            var y = f(ref stream, binary);
            Assert.IsNotNull(y);
        }

        ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed F = (
            ref BufferedStream stream,
            Binary<BufferedStream, Settings_827720117> io) =>
        {
            Issue261_Loop_wih_conditions_fails.ConstructorTests.Test[] result;

            int length0;
            stream.ReserveSize((int)4);
            length0 = stream.Read<int>();
            result = new Issue261_Loop_wih_conditions_fails.ConstructorTests.Test[length0];
            io.LoadedObjectRefs.Add(result);
            int index0;
            Issue261_Loop_wih_conditions_fails.ConstructorTests.Test tempResult;
            index0 = (length0 - 1);

            while (true)
            {
                if (index0 < (int)0)
                {
                    goto void_58225482;
                }
                else
                {

                    // The block result will be assigned to `result[index0]` {
                    tempResult = default(Issue261_Loop_wih_conditions_fails.ConstructorTests.Test);
                    stream.ReserveSize((int)5);

                    if (stream.Read<byte>() == (byte)0)
                    {
                        goto skipRead;
                    }

                    int refIndex;
                    refIndex = stream.Read<int>();

                    if (refIndex != (int)-1)
                    {
                        tempResult = ((Issue261_Loop_wih_conditions_fails.ConstructorTests.Test)io.LoadedObjectRefs[(refIndex - 1)]);
                        goto skipRead;
                    }
                    tempResult = new Issue261_Loop_wih_conditions_fails.ConstructorTests.Test();
                    io.LoadedObjectRefs.Add(tempResult);

                skipRead:
                    result[index0] = tempResult;
                    //} end of block assignment

                    // continue0: // todo: @incomplete - if label is not reference we may safely remove or better comment it in the output
                    index0 = (index0 - 1);
                }
            }
        void_58225482:
            return result;
        };

        internal static class FieldInfoModifier
        {
            internal class TestReadonly
            {
                public TestReadonly()
                {
                }

                public TestReadonly(int v)
                {
                    Value = v;
                }

                public readonly int Value;
            }
        }

        public class ConstructorTests
        {
            public class Test
            {
                public Test()
                { }
            }
        }

        internal class Settings_827720117 { }

        internal static class ReadMethods<T, TStream, TSettingGen>
            where TStream : struct, IBinaryStream
        {
            public delegate T ReadSealed(ref TStream stream, Binary<TStream, TSettingGen> binary);
        }

        internal interface ISerializer
        {
            void Write<T>(T value, Stream outputStream);
            T Read<T>(Stream outputStream);
        }

        internal static class WriteMethods<T, TStream, TSettingGen>
            where TStream : struct, IBinaryStream
        {
            public delegate void WriteSealed(T obj, ref TStream stream, Binary<TStream, TSettingGen> binary);
            public static WriteSealed Method;
            public static int VersionUniqueId;
        }

        internal sealed partial class Binary<TStream, TSettingGen> : ISerializer, IBinary
            where TStream : struct, IBinaryStream
        {
            internal List<object> LoadedObjectRefs { get; } = new List<object>();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Read<T>(Stream outputStream)
            {
                return typeof(T) == typeof(int) ? (T)(object)2 : default(T); // todo: @mock
            }

            public void Write<T>(T value, Stream outputStream)
            {
                throw new NotImplementedException();
            }
        }

        public interface IBinary : IDisposable
        {
        }

        internal interface IBinaryStream : IDisposable
        {
            void ReadFrom(Stream stream);
            void WriteTo(Stream stream);

            void ReserveSize(int sizeNeeded);
            bool Flush();
            void Write(string input);
            void WriteTypeId(Type type);
            void Write<T>(T value) where T : struct;

            string Read();
            T Read<T>() where T : struct;
        }

        internal struct BufferedStream : IBinaryStream, IDisposable
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool Flush()
            {
                throw new NotImplementedException();
            }

            public string Read()
            {
                throw new NotImplementedException();
            }

            public T Read<T>() where T : struct
            {
                return typeof(T) == typeof(int) ? (T)(object)2 : default(T); // todo: @mock
            }

            private static T Read2<T>() where T : struct
            {
                throw new NotImplementedException();
            }

            public void ReadFrom(Stream stream)
            {
            }

            private int _reservedSize;
            public void ReserveSize(int sizeNeeded)
            {
                _reservedSize += sizeNeeded;
            }

            public void Write(string input)
            {
            }

            public void Write<T>(T value) where T : struct
            {
            }

            public void WriteTo(Stream stream)
            {
            }

            public void WriteTypeId(Type type)
            {
            }
        }

        public static byte GetByte() => 0;

#if !LIGHT_EXPRESSION
        [Test]
        public void Should_throw_for_the_equal_expression_of_different_types()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
              Equal(
                Constant(0),
                Call(GetType().GetMethod(nameof(GetByte)))
              )
            );

            StringAssert.StartsWith("The binary operator Equal is not defined for the types", ex.Message);
        }
#endif

#if LIGHT_EXPRESSION
        [Test]
        public void Can_make_convert_and_compile_binary_equal_expression_of_different_types() 
        {
            var e = Lambda<Func<bool>>(
              MakeBinary(ExpressionType.Equal, 
              Call(GetType().GetMethod(nameof(GetByte))),
              Constant((byte)0))
            );

            var s = e.ToExpressionString();
            StringAssert.Contains("Constant((byte)0)", s);

            e.PrintCSharpString();

            var f = e.CompileFast(true);
            f.PrintIL("FEC IL:");
            Assert.IsTrue(f());

            var fs = e.CompileSys();
            fs.PrintIL("System IL:");
            Assert.IsTrue(fs());
        }

        [Test]
        public void Test_find_generic_method_with_the_generic_param() 
        {
            var m = typeof(BufferedStream).GetMethods()
                .Where(x  => x.IsGenericMethod && x.Name == "Write" && x.GetGenericArguments().Length == 1)
                .Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x)
                .Single(x => x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) }));

            Assert.IsNotNull(m);

            var s = new StringBuilder().AppendMethod(m, true, null).ToString();
            Assert.AreEqual("typeof(Issue261_Loop_wih_conditions_fails.BufferedStream).GetMethods().Where(x => x.IsGenericMethod && x.Name == \"Write\" && x.GetGenericArguments().Length == 1).Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x).Single(x => x.GetParameters().Select(y => y.ParameterType).SequenceEqual(new[] { typeof(int) }))", s);
        }

        [Test]
        public void Test_method_to_expression_code_string() 
        {
            var m = typeof(BufferedStream).GetMethods()
                .Where(x  => x.IsGenericMethod && x.Name == "Read" && x.GetParameters().Length == 0 && x.GetGenericArguments().Length == 1)
                .Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x)
                .Single();

            Assert.AreEqual("Read", m.Name);

            var s = new StringBuilder().AppendMethod(m, true, null).ToString();
            Assert.AreEqual("typeof(Issue261_Loop_wih_conditions_fails.BufferedStream).GetMethods().Where(x => x.IsGenericMethod && x.Name == \"Read\" && x.GetParameters().Length == 0 && x.GetGenericArguments().Length == 1).Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x).Single()", s);

            m = typeof(BufferedStream).GetMethods()
              .Single(x => !x.IsGenericMethod && x.Name == "Read" && x.GetParameters().Length == 0);
            Assert.AreEqual("Read", m.Name);

            s = new StringBuilder().AppendMethod(m, true, null).ToString();
            Assert.AreEqual("typeof(Issue261_Loop_wih_conditions_fails.BufferedStream).GetMethods().Single(x => !x.IsGenericMethod && x.Name == \"Read\" && x.GetParameters().Length == 0)", s);

            m = typeof(BufferedStream).GetMethods(BindingFlags.NonPublic|BindingFlags.Static)
                .Where(x  => x.IsGenericMethod && x.Name == "Read2" && x.GetParameters().Length == 0 && x.GetGenericArguments().Length == 1)
                .Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x)
                .Single();
            Assert.AreEqual("Read2", m.Name);

            s = new StringBuilder().AppendMethod(m, true, null).ToString();
            Assert.AreEqual("typeof(Issue261_Loop_wih_conditions_fails.BufferedStream).GetMethods(BindingFlags.NonPublic|BindingFlags.Static).Where(x => x.IsGenericMethod && x.Name == \"Read2\" && x.GetParameters().Length == 0 && x.GetGenericArguments().Length == 1).Select(x => x.IsGenericMethodDefinition ? x.MakeGenericMethod(typeof(int)) : x).Single()", s);
        }

        [Test]
        public void Test_nested_generic_type_output() 
        {
            var s = typeof(ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed)
                .ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));

            Assert.AreEqual("ReadMethods<ConstructorTests.Test[], BufferedStream, Settings_827720117>.ReadSealed", s);
        }

        [Test]
        public void Test_triple_nested_non_generic() 
        {
            var s = typeof(A<int>.B<string>.Z).ToCode(true);
            Assert.AreEqual("Issue261_Loop_wih_conditions_fails.A<int>.B<string>.Z", s);

            s = typeof(A<int>.B<string>.Z).ToCode();
            Assert.AreEqual("FastExpressionCompiler.LightExpression.IssueTests.Issue261_Loop_wih_conditions_fails.A<int>.B<string>.Z", s);

            s = typeof(A<int>.B<string>.Z[]).ToCode(true);
            Assert.AreEqual("Issue261_Loop_wih_conditions_fails.A<int>.B<string>.Z[]", s);
            
            s = typeof(A<int>.B<string>.Z[]).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));
            Assert.AreEqual("A<int>.B<string>.Z[]", s);
        }

        [Test]
        public void Test_triple_nested_open_generic() 
        {
            var s = typeof(A<>).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));
            Assert.AreEqual("A<>", s);
            
            s = typeof(A<>).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""), true);
            Assert.AreEqual("A<X>", s);

            s = typeof(A<>.B<>).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));
            Assert.AreEqual("A<>.B<>", s);

            s = typeof(A<>.B<>.Z).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));
            Assert.AreEqual("A<>.B<>.Z", s);

            s = typeof(A<>.B<>.Z).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""), true);
            Assert.AreEqual("A<X>.B<Y>.Z", s);
        }

        [Test]
        public void Test_non_generic_classes() 
        {
            var s = typeof(A.B.C).ToCode(true, (_, x) => x.Replace("Issue261_Loop_wih_conditions_fails.", ""));
            Assert.AreEqual("A.B.C", s);
        }

        class A
        {
            public class B
            {
                public class C {}
            }
        }

        class A<X> 
        {
            public class B<Y> 
            {
                public class Z {}
            }
        }

        public interface IFoo
        {
            void Nah(int i);
        }

        public class Foo : IFoo
        {
            public int MethodIndex = -1;

            void IFoo.Nah(int i)             { MethodIndex = 0; }
            public void Nah(int i)           { MethodIndex = 1; }
            public void Nah(ref int d)       { MethodIndex = 2; }
            public void Nah<T>(ref int d)    { MethodIndex = 3; }
            public void Nah(ref object d)    { MethodIndex = 4; }
        }

        public class Bar : Foo
        {
            public void Nah(double d)        { MethodIndex = 5; }
            public void Nah(ref double d)    { MethodIndex = 6; }
            public void Nah<T>(ref double d) { MethodIndex = 7; }
            public void Nah<T>(ref T d)      { MethodIndex = 8; }
        }

        [Test]
        public void FindMethodOrThrow_in_the_class_hierarchy()
        {
            var bar = new Bar();

            var ex = Assert.Throws<InvalidOperationException>(() =>
              Lambda<Action>(Call(Constant(bar), "Nah", new Type[] { typeof(int) }, Constant(5))));
            StringAssert.StartsWith("More than one", ex.Message);

            ex = Assert.Throws<InvalidOperationException>(() =>
              SysExpr.Lambda<Action>(SysExpr.Call(SysExpr.Constant(bar), "Nah", new Type[] { typeof(int) }, SysExpr.Constant(5))));
            StringAssert.StartsWith("More than one", ex.Message);

            var e = Lambda<Action>(Call(Constant(bar), "Nah", new Type[] { typeof(string) }, Constant("x")));
            e.CompileFast(true)();
            Assert.AreEqual(8, bar.MethodIndex);

            var es = SysExpr.Lambda<Action>(SysExpr.Call(SysExpr.Constant(bar), "Nah", new Type[] { typeof(string) }, SysExpr.Constant("y")));
            es.Compile()();
            Assert.AreEqual(8, bar.MethodIndex);

            ex = Assert.Throws<InvalidOperationException>(() =>
              Lambda<Action>(Call(Constant(bar), "Nah", new Type[] { typeof(double) }, Constant((double)42.3))));
            StringAssert.StartsWith("More than one", ex.Message);

            ex = Assert.Throws<InvalidOperationException>(() =>
              SysExpr.Lambda<Action>(SysExpr.Call(SysExpr.Constant(bar), "Nah", new Type[] { typeof(double) }, SysExpr.Constant((double)42.3))));
            StringAssert.StartsWith("More than one", ex.Message);

            ex = Assert.Throws<InvalidOperationException>(() =>
              Lambda<Action>(Call(Constant(bar), "Nah", Type.EmptyTypes, Constant((double)42.3))));
            StringAssert.StartsWith("More than one", ex.Message);

            ex = Assert.Throws<InvalidOperationException>(() =>
              SysExpr.Lambda<Action>(SysExpr.Call(SysExpr.Constant(bar), "Nah", Type.EmptyTypes, SysExpr.Constant((double)42.3))));
            StringAssert.StartsWith("More than one", ex.Message);

            e = Lambda<Action>(Call(Constant(bar), "Nah", Type.EmptyTypes, Constant(null)));
            e.CompileFast(true)();
            Assert.AreEqual(4, bar.MethodIndex);

            es = SysExpr.Lambda<Action>(SysExpr.Call(SysExpr.Constant(bar), "Nah", Type.EmptyTypes, SysExpr.Constant(null)));
            es.Compile()();
            Assert.AreEqual(4, bar.MethodIndex);
        }
#endif
    }
}