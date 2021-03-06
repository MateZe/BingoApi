# BingoApi
API based on Rest for my project "Lucky 6 - Bingo 35/48"

I'm using local server with SQL Server Express 2019
"Korisnici" - Table for user data
"Runda"  - Table for round data (Round of 35 numbers, beginning and end of the round )
"Listic" - Table for ticket that user played (Id's of the user and round, 6 numbers selected by the user, correctly guessed numbers, bet amount and profit)
SQL query code for tables that I'm using:

- "Korisnici" table:

            CREATE TABLE [dbo].[Korisnici](
              [Id_korisnika] [int] IDENTITY(1,1) NOT NULL,
              [Ime_prezime] [varchar](50) NOT NULL,
              [Balans] [decimal](18, 2) NOT NULL,
             CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED 
            (
              [Id_korisnika] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            GO

            ALTER TABLE [dbo].[Korisnici] ADD  CONSTRAINT [DF_Korisnici_Balans]  DEFAULT ((0)) FOR [Balans]
            GO



- "Runda" table:

            CREATE TABLE [dbo].[Runda](
              [Id_runde] [int] IDENTITY(1,1) NOT NULL,
              [Izvuceni_brojevi] [varchar](104) NOT NULL,
              [Pocetak_runde] [datetime2](0) NOT NULL,
              [Kraj_runde] [datetime2](0) NOT NULL,
             CONSTRAINT [PK_Runda] PRIMARY KEY CLUSTERED 
            (
              [Id_runde] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON      [PRIMARY]
            ) ON [PRIMARY]


            GO


- "Listic" table:

            CREATE TABLE [dbo].[Listic](
              [Id_listica] [int] IDENTITY(1,1) NOT NULL,
              [Korisnik_id] [int] NOT NULL,
              [Runda_id] [int] NOT NULL,
              [Odigrani_brojevi] [varchar](35) NOT NULL,
              [Izvuceni_brojevi] [varchar](35) NULL,
              [Ulog] [decimal](18, 2) NOT NULL,
              [Dobit] [decimal](18, 2) NOT NULL,
             CONSTRAINT [PK_Listic] PRIMARY KEY CLUSTERED 
            (
              [Id_listica] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            GO

            ALTER TABLE [dbo].[Listic]  WITH CHECK ADD  CONSTRAINT [FK__Listic__korisnik__2A4B4B5E] FOREIGN KEY([Korisnik_id])
            REFERENCES [dbo].[Korisnici] ([Id_korisnika])
            GO

            ALTER TABLE [dbo].[Listic] CHECK CONSTRAINT [FK__Listic__korisnik__2A4B4B5E]
            GO

            ALTER TABLE [dbo].[Listic]  WITH CHECK ADD  CONSTRAINT [FK__Listic__runda_id__2B3F6F97] FOREIGN KEY([Runda_id])
            REFERENCES [dbo].[Runda] ([Id_runde])
            GO

            ALTER TABLE [dbo].[Listic] CHECK CONSTRAINT [FK__Listic__runda_id__2B3F6F97]
            GO
