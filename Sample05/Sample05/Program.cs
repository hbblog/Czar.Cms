
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            //test_insert();
            //test_mult_insert();
            //test_delete();
            //test_mult_delete();
            //test_update();
            //test_mult_update();
            //test_select();
            //test_mult_select();
            //test_insert2();
            test_select_content_with_comment();
            Console.ReadKey();
        }

        /// <summary>
        /// 关联查询
        /// </summary>
        static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from [dbo].[content] where id = @id;
select * from comment where content_id = @id";

                using (var result = conn.QueryMultiple(sql, new { id = 5 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }


            }
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        static void test_select()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from [dbo].[content] where id=@id";

                var result = conn.QueryFirstOrDefault<Content>(sql, new { id = 5 });

                Console.WriteLine($"test_select:查询到的数据为:{result.title}");
            }
        }

        /// <summary>
        /// 查询多条数据
        /// </summary>
        static void test_mult_select()
        {
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"select * from [dbo].[content] where id in @ids";

                var result = conn.Query<Content>(sql, new { ids = new int[] { 5, 6, 7 } });
                foreach (var item in result)
                {
                    Console.WriteLine($"test_select:查询到的数据为:{item.title}");
                }
            }
        }


        /// <summary>
        /// 添加一条数据
        /// </summary>
        static void test_insert2()
        {
            var comment = new Comment
            {
                content_id = 5,
                content = "内容2",
                add_time = DateTime.Now,
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"INSERT INTO [dbo].[comment]
                                       (content_id,[content],[add_time])
                                 VALUES
                                       (@content_id,@content,@add_time)";

                var result = conn.Execute(sql, comment);
                Console.WriteLine($"test_insert:插入了{result}条数据！");
            }
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        static void test_update()
        {
            var content = new Content
            {
                id = 5,
                title = "标题5",
                content = "内容5"
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"update [dbo].[content] set title=@title,content=@content where id=@id";

                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_update:修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        static void test_mult_update()
        {
            List<Content> list = new List<Content>() {
                new Content
            {
                id = 6,
                title = "标题6",
                content = "内容6"
            },new Content
            {
                id = 7,
                title = "标题7",
                content = "内容7"
            }
            };

            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"update [dbo].[content] set title=@title,content=@content where id=@id";

                var result = conn.Execute(sql, list);
                Console.WriteLine($"test_mult_update:修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        static void test_insert()
        {
            var content = new Content
            {
                title = "标题2",
                content = "内容2",
                status = "1",
                add_time = DateTime.Now,
                modify_time = DateTime.Now
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"INSERT INTO [dbo].[content]
                                       ([title],[content],[status],[add_time],[modify_time])
                                 VALUES
                                       (@title,@CONTENT,@STATUS,@add_time,@modify_time)";

                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_insert:插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        static void test_mult_insert()
        {
            List<Content> list = new List<Content>() {
                new Content
                {
                    title = "批量插入标题1",
                    content = "批量插入内容1",
                    status = "1",
                    add_time = DateTime.Now,
                    modify_time = DateTime.Now
                },new Content
                {
                    title = "批量插入标题2",
                    content = "批量插入内容2",
                    status = "1",
                    add_time = DateTime.Now,
                    modify_time = DateTime.Now
                }
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"INSERT INTO [dbo].[content]
                                       ([title],[content],[status],[add_time],[modify_time])
                                 VALUES
                                       (@title,@CONTENT,@STATUS,@add_time,@modify_time)";

                var result = conn.Execute(sql, list);
                Console.WriteLine($"test_insert:插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        static void test_delete()
        {
            var content = new Content
            {
                id = 1
            };
            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"delete from [dbo].[content] where id=@id";

                var result = conn.Execute(sql, content);
                Console.WriteLine($"test_delete:删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        static void test_mult_delete()
        {
            List<Content> list = new List<Content>() {
                  new Content
                 {
                     id = 3
                 },
                    new Content
                {
                    id = 4
                }
            };

            using (var conn = new SqlConnection("Data Source=.;User ID=sa;Password=123;Initial Catalog=Test;Pooling=true;Max Pool Size=100;"))
            {
                string sql = @"delete from [dbo].[content] where id=@id";

                var result = conn.Execute(sql, list);
                Console.WriteLine($"test_mult_delete:删除了{result}条数据！");
            }
        }

    }
}
