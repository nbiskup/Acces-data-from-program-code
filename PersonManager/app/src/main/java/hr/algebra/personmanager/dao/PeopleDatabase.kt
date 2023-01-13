package hr.algebra.personmanager.dao

import android.content.Context
import androidx.room.*

@Database(entities = [Person::class], version = 2, autoMigrations = [ AutoMigration(from = 1, to = 2) ])
@TypeConverters(DateConverter::class)
abstract class PeopleDatabase : RoomDatabase() {

    abstract fun personDao() : PersonDao

    companion object {
        @Volatile private var INSTANCE: PeopleDatabase? = null

        fun getInstance(context: Context) =
            INSTANCE ?: synchronized(PeopleDatabase::class.java) {
                INSTANCE ?: buildDatabase(context)?.also { INSTANCE = it }
            }

        private fun buildDatabase(context: Context) = Room.databaseBuilder(
            context,
            PeopleDatabase::class.java,
            "people.db"
        ).build()

    }

}